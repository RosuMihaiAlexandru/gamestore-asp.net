using Gamesmarket.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Gamesmarket.DAL
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<long>, long>
    {// Class for working with a database
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        // Managing collection of objects in database with Entity Framework
        public DbSet<Game> Games { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed roles
            var roleManager = modelBuilder.Entity<IdentityRole<long>>().HasData(
                new IdentityRole<long> { Id = 1, Name = "User", NormalizedName = "USER" },
                new IdentityRole<long> { Id = 2, Name = "Moderator", NormalizedName = "MODERATOR" },
                new IdentityRole<long> { Id = 3, Name = "Administrator", NormalizedName = "ADMINISTRATOR" }
            );

            // Link the User and Cart entities
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasOne(u => u.Cart)
                    .WithOne(b => b.User)
                    .HasForeignKey<Cart>(b => b.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Cart entity configuration
            modelBuilder.Entity<Cart>(builder =>
            {
                builder.ToTable("Carts").HasKey(x => x.Id);

                builder.HasData(new Cart()
                {
                    Id = 1,
                    UserId = 1
                });
            });

            // Order entity configuration
            modelBuilder.Entity<Order>(builder =>
            {
                builder.ToTable("Orders").HasKey(x => x.Id);

                builder.HasOne(r => r.Cart).WithMany(t => t.Orders)
                    .HasForeignKey(r => r.CartId);
            });

            // Seed admin user
            var adminUser = new User
            {
                Id = 1,
                Name = "Admin",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true
            };
            var passwordHasher = new PasswordHasher<User>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Qwe!23");

            modelBuilder.Entity<User>().HasData(adminUser);// Add Admin user to db

            // Assign Admin role to the admin user
            modelBuilder.Entity<IdentityUserRole<long>>().HasData(
                new IdentityUserRole<long> { UserId = adminUser.Id, RoleId = 3 } // RoleId 3 = "Administrator" role
            );
        }
    }
}
