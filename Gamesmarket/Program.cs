using Gamesmarket.Service.Implementations;
using Gamesmarket.Service.Interfaces;
using GamesMarket.DAL;
using GamesMarket.DAL.Interfaces;
using GamesMarket.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
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
    options.UseSqlServer(connectionString)

);
//Setting up dependency injection for the interface and its implementation
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddCors(o => o.AddPolicy("frontend", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDefaultFiles(); // Middleware to handle requests when a client tries to retrieve the contents of a directory
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseCors("frontend");

app.UseRouting();
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
