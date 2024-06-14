namespace Gamesmarket.Configurations
{
    public static class CorsConfiguration
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("frontend", opt =>
            {
                opt.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
            }));
        }
    }

}
