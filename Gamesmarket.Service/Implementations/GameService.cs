using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;
using Gamesmarket.Domain.ViewModel.Game;
using Gamesmarket.Service.Interfaces;
using GamesMarket.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Gamesmarket.Service.Implementations
{
    public class GameService : IGameService
    {
        private readonly IBaseRepository<Game> _gameRepository;
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        //DI of IGameRepository in GameService class constructor
        public GameService(IBaseRepository<Game> gameRepository, IImageService imageService, IWebHostEnvironment hostingEnvironment)
        {
            _gameRepository = gameRepository;
            _imageService = imageService;
            _hostingEnvironment = hostingEnvironment;
        }

        // Method for getting one game by id from the repository or an error
        public async Task<IBaseResponse<Game>> GetGame(int id)
        {
            var baseResponse = new BaseResponse<Game>();//New object for operations with Game type objects.
			try
            {
                var game = await _gameRepository.Get(id);//Getting a game by ID
				if (game == null)//If the game is not found, set appropriate status and description
				{
					baseResponse.Description = "Game not found";
                    baseResponse.StatusCode = StatusCode.GameNotFound;
                    return baseResponse;
                }
				//If the game is found, assign it to the response data
				baseResponse.Data = game;
                return baseResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetGame: {ex}");
                return new BaseResponse<Game>
                {
                    Description = $"[GetGame] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        // Method for createing game  
        public async Task<IBaseResponse<GameViewModel>> CreateGame(GameViewModel gameViewModel)
        {
            var baseResponse = new BaseResponse<GameViewModel>();
            try
            {
                var imageResponse = await SaveGameImage(gameViewModel.ImageFile);
                if (imageResponse.StatusCode != StatusCode.OK)
                {
                    var concreteResponse = (BaseResponse<string>)imageResponse; // Clarification that we take Description from BaseResponse
                    baseResponse.Description = concreteResponse.Description;
                    baseResponse.StatusCode = imageResponse.StatusCode;
                    return baseResponse;
                }

                var game = new Game()
                {//Create a new Game object based on the GameViewModel
                    Description = gameViewModel.Description,
                    ReleaseDate = gameViewModel.ReleaseDate,
                    Developer = gameViewModel.Developer,
                    Price = gameViewModel.Price,
                    Name = gameViewModel.Name,
                    GameGenre = (GameGenre)Convert.ToInt32(gameViewModel.GameGenre),
                    ImagePath = imageResponse.Data,
                };
                await _gameRepository.Create(game); //Call repository method to create the game

                // Show data of created game after game created
                var gameViewModelData = new GameViewModel
                {
                    Id = game.Id,
                    Description = game.Description,
                    ReleaseDate = game.ReleaseDate,
                    Developer = game.Developer,
                    Price = game.Price,
                    Name = game.Name,
                    GameGenre = ((int)game.GameGenre).ToString(),
                };
                baseResponse.Data = gameViewModelData;
            }
            catch (InvalidOperationException ex) // Exception due to invalid image format or size
            {
                baseResponse.Description = ex.Message;
                baseResponse.StatusCode = StatusCode.InvalidData;
                return baseResponse;
            }
            catch (Exception ex) // A general catch-all for any other exceptions
            {
                baseResponse.Description = $"[CreateGame] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }
            return baseResponse;
        }

        // Method for deleting one game by id 
        public async Task<IBaseResponse<bool>> DeleteGame(int id)
        {
            var baseResponse = new BaseResponse<bool>()
            {
                Data = true
            };
            try
            {
                var game = await _gameRepository.Get(id);
                if (game == null)
                {
                    baseResponse.Description = "Game not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    baseResponse.Data = false;

                    return baseResponse;
                }

                string relativeImagePath = game.ImagePath; // Get the image path

                // Combine the relative path with the root path
                string rootPath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot");
                string fullPath = Path.Combine(rootPath, relativeImagePath);

                // Delete image
                if (!string.IsNullOrEmpty(relativeImagePath))
                {
                    Console.WriteLine("Deleting image file...");
                    try
                    {
                        File.Delete(fullPath);
                        Console.WriteLine("Image file deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting image file: {ex.Message}");
                        throw; // Exception to prevent continuing db deletion
                    }
                }
                await _gameRepository.Delete(game);// Call method to delete the game

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[DeleteGame] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        // Method for getting games by its name or developer
        public async Task<IBaseResponse<IEnumerable<Game>>> SearchGames(string searchQuery)
        {
            var baseResponse = new BaseResponse<IEnumerable<Game>>();
            try
            {
                var games = await _gameRepository.GetAll().Where(g => g.Name == searchQuery || g.Developer == searchQuery).ToListAsync();

                if (games == null || !games.Any())
                {
                    baseResponse.Description = "Game not found";
                    baseResponse.StatusCode = StatusCode.GameNotFound;
                    return baseResponse;
                }
                baseResponse.Data = games;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Game>>
                {
                    Description = $"[GetGame] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        // Method for getting a list of games 
        public async Task<IBaseResponse<IEnumerable<Game>>> GetGames()
        {
            var baseResponse = new BaseResponse<IEnumerable<Game>>();
            try
            {
                var games = await _gameRepository.Select();//Get a list of games from repository
				if (games.Count == 0)
                {
                    baseResponse.Description = "Found 0 elements";
                    baseResponse.StatusCode = StatusCode.GameNotFound;

                    return baseResponse;
                }
				//If games are found, assign the list to the response data
				baseResponse.Data = games;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<IEnumerable<Game>>
                {
                    Description = $"[GetGames] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

		// Method for data editing 
		public async Task<IBaseResponse<Game>> Edit(int id, GameViewModel model)
		{
            var baseResponse = new BaseResponse<Game>();
            try
            {
                var game = await _gameRepository.Get(id);
                if (game == null)
                {
                    baseResponse.StatusCode = StatusCode.GameNotFound;
					baseResponse.Description = "Game not found";
					return baseResponse;
                }
                // Delete the existing image file
                if (!string.IsNullOrEmpty(game.ImagePath))
                {
                    try
                    {
                        Console.WriteLine("Deleting existing image file...");
                        string existingImagePath = Path.Combine(_hostingEnvironment.WebRootPath, game.ImagePath);
                        File.Delete(existingImagePath);
                        Console.WriteLine("Existing image file deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting existing image file: {ex.Message}");
                    }
                }

                //Updated game properties using data from GameViewModel
                game.Description = model.Description;
                game.Developer = model.Developer;
                game.ReleaseDate = model.ReleaseDate;
				game.Price = model.Price;
                game.Name = model.Name; 
                game.GameGenre = (GameGenre)Convert.ToInt32(model.GameGenre);

                // Handle image update
                if (model.ImageFile != null)
                {
                    try
                    {
                        var imageResponse = await SaveGameImage(model.ImageFile);
                        if (imageResponse.StatusCode == StatusCode.OK)
                        {
                            game.ImagePath = imageResponse.Data; // Extract the string path from the response
                        }
                        else
                        {
                            baseResponse.Description = "Game not found";
                            baseResponse.StatusCode = imageResponse.StatusCode;
                            return baseResponse;
                        }
                    }
                    catch (InvalidOperationException ex) // Exception due to invalid image format or size
                    {
                        baseResponse.Description = ex.Message;
                        baseResponse.StatusCode = StatusCode.InvalidData;
                        return baseResponse;
                    }

                }
                // Call the repository method to update the game in db
                await _gameRepository.Update(game);
                baseResponse.Data = game;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<Game>
				{
					Description = $"[Edit] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
		}

        // Method for image saving 
        public async Task<IBaseResponse<string>> SaveGameImage(IFormFile imageFile)
        {
            // Save the image and return the path
            return await _imageService.SaveImageAsync(imageFile, "game");
        }
    }
}
