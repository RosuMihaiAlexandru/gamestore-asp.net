using Gamesmarket.DAL.Repositories;
using Gamesmarket.Domain.Entity;
using Gamesmarket.Service.Implementations;
using Gamesmarket.Service.Interfaces;
using GamesMarket.DAL;
using GamesMarket.DAL.Interfaces;
using GamesMarket.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();

// DataBase connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//Setting up dependency injection for the interface and its implementation
builder.Services.AddScoped<IBaseRepository<Game>, GameRepository>();
builder.Services.AddScoped<IBaseRepository<Cart>, CartRepository>();
builder.Services.AddScoped<IBaseRepository<Order>, OrderRepository>();

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// JWT Bearer authentication
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {// Configure JWT Bearer authentication options
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"]!,
            ValidAudience = builder.Configuration["Jwt:Audience"]!,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!))
        };
    });

builder.Services.AddAuthorization(options =>
{
    // Adding a policy for authorized users with "Admin" role
    options.AddPolicy("AdminPolicy", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .RequireRole("Administrator")
        .Build());

    // Adding a policy for authorized users with "Moderator"and "Admin" roles
    options.AddPolicy("StaffPolicy", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .RequireRole("Moderator", "Administrator")
        .Build());

    // Authorization policy for default authenticated users ("User" role)
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

// Identity setup for user management
builder.Services.AddIdentity<User, IdentityRole<long>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserManager<UserManager<User>>()
    .AddSignInManager<SignInManager<User>>();

// Cross-Origin Resource Sharing configuration
builder.Services.AddCors(o => o.AddPolicy("frontend", opt =>
{
    opt.WithOrigins("http://localhost:3000")
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowCredentials();
}));

// Swagger - localhost:7202/swagger/index.html
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Gamesmarket", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme// Configure security definition for Bearer token
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement// Configure security definition for Bearer token
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles(); // Middleware to handle requests when a client tries to retrieve the contents of a directory

app.UseRouting();
app.UseCors("frontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // Specifies to use controllers to process requests
app.MapFallbackToFile("/index.html"); // If no previous middleware has processed the request, this middleware returns the file ("")

// Creates a scope for accessing services in the dependency container, guaranteed db creation at application startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
