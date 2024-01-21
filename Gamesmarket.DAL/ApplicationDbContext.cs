using Gamesmarket.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GamesMarket.DAL
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<long>, long>
    {// Class for working with a database
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        // Managing collection of objects in database with Entity Framework
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed roles
            var roleManager = modelBuilder.Entity<IdentityRole<long>>().HasData(
                new IdentityRole<long> { Id = 1, Name = "User", NormalizedName = "USER" },
                new IdentityRole<long> { Id = 2, Name = "Moderator", NormalizedName = "MODERATOR" },
                new IdentityRole<long> { Id = 3, Name = "Administrator", NormalizedName = "ADMINISTRATOR" }
            );
        }

    }
}
