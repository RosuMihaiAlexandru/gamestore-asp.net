using FluentAssertions;
using GamesMarket.DAL;
using Gamesmarket.Domain.Entity;
using Gamesmarket.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Gamesmarket.Domain.Enum;

namespace Gamesmarket.Tests.Repository
{
    public class OrderRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Orders.CountAsync() <= 0)
            {
                var user = new User // Create a user
                {
                    Id = 2,
                    Name = "Test User",
                    UserName = "testuser@example.com",
                    Email = "testuser@example.com",
                };
                await databaseContext.Users.AddAsync(user);

                var cart = new Cart // Create a cart associated with the user
                {
                    Id = 2,
                    UserId = user.Id
                };
                await databaseContext.Carts.AddAsync(cart);

                var game = new Game // Create a game
                {
                    Name = "Game order",
                    Developer = "dev",
                    Description = "game game game",
                    Price = 123,
                    ReleaseDate = new DateTime(2003, 3, 1),
                    GameGenre = GameGenre.Action,
                    ImagePath = "TestPhotos/testpic.jpg"
                };
                await databaseContext.Games.AddAsync(game);

                var order = new Order // Create an order associated with the cart
                {
                    GameId = 1,
                    DateCreated = DateTime.UtcNow,
                    Email = "test@example.com",
                    Name = "Test Order",
                    CartId = cart.Id
                };
                await databaseContext.Orders.AddAsync(order);
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }

        [Fact]
        public async Task OrderRepository_Get_ReturnsOrder()
        {
            // Arrange
            var orderId = 1;
            var dbContext = await GetDatabaseContext();
            var orderRepository = new OrderRepository(dbContext);

            // Act
            var result = await orderRepository.Get(orderId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Order>();
            result.Id.Should().Be(orderId);
        }

        [Fact]
        public async Task OrderRepository_Create_ReturnsTrue()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var orderRepository = new OrderRepository(dbContext);
            var newOrder = new Order
            {
                GameId = 1,
                DateCreated = DateTime.UtcNow,
                Email = "test@example.com",
                Name = "New Test Order",
                CartId = 2
            };

            // Act
            var result = await orderRepository.Create(newOrder);

            // Assert
            result.Should().BeTrue();
            var createdOrder = await dbContext.Orders.FindAsync(newOrder.Id);
            createdOrder.Should().NotBeNull();
        }

        [Fact]
        public async Task OrderRepository_Delete_ReturnsTrue()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var orderRepository = new OrderRepository(dbContext);
            var orderToDelete = await dbContext.Orders.FirstAsync(); // Get an order to delete

            // Act
            var result = await orderRepository.Delete(orderToDelete);

            // Assert
            result.Should().BeTrue();
            var deletedOrder = await dbContext.Orders.FindAsync(orderToDelete.Id);
            deletedOrder.Should().BeNull(); // Assert the order is no longer present
        }

        [Fact]
        public async Task OrderRepository_GetAll_ReturnsQueryableOfOrders()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var orderRepository = new OrderRepository(dbContext);

            // Act
            var result = orderRepository.GetAll();

            // Assert
            result.Should().NotBeNull();
        }
    }
}
