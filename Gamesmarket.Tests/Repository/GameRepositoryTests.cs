using FluentAssertions;
using Gamesmarket.DAL;
using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Enum;
using Gamesmarket.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Gamesmarket.Tests.Repository
{
    public class GameRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDatabaseContext()// Creating InMemoryDatabase based on my original ApplicationDbContext
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Games.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Games.Add(
                    new Game()
                    {
                        Name = "Game",
                        Developer = "dev",
                        Description = "game game game",
                        Price = 123,
                        ReleaseDate = new DateTime(2003, 3, 1),
                        GameGenre = GameGenre.RPG,
                        ImagePath = "TestPhotos/testpic.jpg"
                    }
                    );
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async void GameRepository_GetGame_ReturnGame()
        {
            //Arrange
            var id = 1;
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);

            //Act
            var result = await gameRepository.Get(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Game>();
        }

        [Fact]
        public async void GameRepository_CreateGame_ReturnsTrue()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);
            var newGame = new Game
            {
                Name = "Test Game",
                Developer = "Test Developer",
                Description = "This is a test game",
                Price = 19.99m,
                ReleaseDate = DateTime.UtcNow,
                GameGenre = GameGenre.Adventure,
                ImagePath = "TestPhotos/testpic.jpg"
            };

            //Act
            var result = await gameRepository.Create(newGame);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void GameRepository_Select_ReturnsListOfGames()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);

            //Act
            var result = await gameRepository.Select();

            //Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async void GameRepository_Update_ReturnsUpdatedGame()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);
            var newGame = new Game
            {
                Name = "Game for update",
                Developer = "Dev",
                Description = "This is a game",
                Price = 19.99m,
                ReleaseDate = DateTime.UtcNow,
                GameGenre = GameGenre.Adventure,
                ImagePath = "TestPhotos/testpic.jpg"
            };
            await dbContext.Games.AddAsync(newGame);
            await dbContext.SaveChangesAsync();

            newGame.Name = "Updated Game";
            newGame.Description = "This is the updated game description";

            //Act
            var result = await gameRepository.Update(newGame);

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Updated Game");
            result.Description.Should().Be("This is the updated game description");
        }

        [Fact]
        public async Task GameRepository_Delete_ReturnsTrue()
        {
            // Arrange
            var dbContext = await GetDatabaseContext(); 
            var gameRepository = new GameRepository(dbContext);
            var newGame = new Game
            {
                Name = "Game to Delete",
                Developer = "Dev to Delete",
                Description = "This game will be deleted",
                Price = 19.99m,
                ReleaseDate = DateTime.UtcNow,
                GameGenre = GameGenre.Adventure,
                ImagePath = "TestPhotos/testpic.jpg"
            };
            await dbContext.Games.AddAsync(newGame);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await gameRepository.Delete(newGame);

            // Assert
            result.Should().BeTrue();
            var deletedGame = await dbContext.Games.FindAsync(newGame.Id);
            deletedGame.Should().BeNull(); // Assert the game is no longer present in the database
        }

        [Fact]
        public async void GameRepository_GetAll_ReturnsQueryableOfGames()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var gameRepository = new GameRepository(dbContext);

            //Act
            var result = gameRepository.GetAll();

            //Assert
            result.Should().NotBeNull();
        }
    }
}