using Gamesmarket.DAL;
using Gamesmarket.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureLogging();

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

builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();

builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureCors();
builder.Services.ConfigureSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles(); // Serve default files (like index.html) when a directory is requested.

app.UseRouting();
app.UseCors("frontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // Map API controllers to handle incoming HTTP requests
app.MapFallbackToFile("/index.html"); // Serve index.html for all other routes, enabling client-side routing for the SPA
    
// Creates a scope for accessing services in the dependency container, guaranteed db creation at application startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

app.Run();