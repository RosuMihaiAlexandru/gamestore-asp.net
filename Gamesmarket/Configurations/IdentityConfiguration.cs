using Gamesmarket.DAL;
using Gamesmarket.Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace Gamesmarket.Configurations
{
    public static class IdentityConfiguration
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {// Identity setup for user management
            services.AddIdentity<User, IdentityRole<long>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<UserManager<User>>()
                .AddSignInManager<SignInManager<User>>();
        }
    }
}
