using FluentAssertions;
using GamesMarket.DAL;
using Gamesmarket.Domain.Entity;
using Gamesmarket.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Gamesmarket.Tests.Repository
{
    public class CartRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Carts.CountAsync() <= 0)
            {
                var user = new User // Create a user
                {
                    Id = 1,
                    Name = "Test User",
                    UserName = "testuser@example.com",
                    Email = "testuser@example.com",
                };
                await databaseContext.Users.AddAsync(user);

                var cart = new Cart // Create a cart associated with the user
                {
                    Id = 1,
                    UserId = user.Id
                };
                await databaseContext.Carts.AddAsync(cart);

                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        [Fact]
        public async Task CartRepository_Get_ReturnsCart()
        {
            // Arrange
            var cartId = 1;
            var dbContext = await GetDatabaseContext();
            var cartRepository = new CartRepository(dbContext);

            // Act
            var result = await cartRepository.Get(cartId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Cart>();
            result.Id.Should().Be(cartId);
        }

        [Fact]
        public async Task CartRepository_Create_ReturnsTrue()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var cartRepository = new CartRepository(dbContext);
            var newCart = new Cart
            {
                UserId = 1
            };

            // Act
            var result = await cartRepository.Create(newCart);

            // Assert
            result.Should().BeTrue();
            var createdCart = await dbContext.Carts.FindAsync(newCart.Id);
            createdCart.Should().NotBeNull();
        }

        [Fact]
        public async Task CartRepository_GetAll_ReturnsQueryableOfCarts()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var cartRepository = new CartRepository(dbContext);

            // Act
            var result = cartRepository.GetAll();

            // Assert
            result.Should().NotBeNull();
        }
    }
}
