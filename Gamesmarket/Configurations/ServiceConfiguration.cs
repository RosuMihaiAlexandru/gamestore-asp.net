using Gamesmarket.DAL.Interfaces;
using Gamesmarket.DAL.Repositories;
using Gamesmarket.Domain.Entity;
using Gamesmarket.Service.Implementations;
using Gamesmarket.Interfaces.Services;
using Gamesmarket.Interfaces.Utilities;
using Gamesmarket.Utilities.Image;

namespace Gamesmarket.Configurations
{
    public static class ServiceConfiguration
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Game>, GameRepository>();
            services.AddScoped<IBaseRepository<Cart>, CartRepository>();
            services.AddScoped<IBaseRepository<Order>, OrderRepository>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IImageValidator, ImageValidator>();
            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<IFilterService, FilterService>();
            services.AddScoped<ISortService, SortService>();
        }
    }
}
