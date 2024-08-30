using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;
using Gamesmarket.Domain.ViewModel.Game;
using Gamesmarket.Interfaces.Services;
using Gamesmarket.DAL.Interfaces;
using Gamesmarket.Utilities.Games;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

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
                var game = await _gameRepository.Get(id);
                if (game == null)
                {
                    baseResponse.Description = "Game not found";
                    baseResponse.StatusCode = StatusCode.GameNotFound;
                    return baseResponse;
                }
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

                var game = GameMappingUtilities.CreateGameFromViewModel(gameViewModel, imageResponse.Data); //Create a new Game object based on the GameViewModel
                await _gameRepository.Create(game); //Call repository method to create the game

                // Show data of created game after game created
                baseResponse.Data = GameMappingUtilities.CreateViewModelFromGame(game);
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[CreateGame] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }
            return baseResponse;
        }

        // Method for deleting one game by id 
        public async Task<IBaseResponse<bool>> DeleteGame(int id)
        {
            var baseResponse = new BaseResponse<bool> { Data = true };
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

                FileUtilities.DeleteImageFile(game.ImagePath, _hostingEnvironment);
                await _gameRepository.Delete(game);
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

        // Method for getting a list of games 
        public async Task<IBaseResponse<IEnumerable<Game>>> GetGames()
        {
            var baseResponse = new BaseResponse<IEnumerable<Game>>();
            try
            {
                var games = await _gameRepository.Select();
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
            catch (Exception ex)
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

                FileUtilities.DeleteImageFile(game.ImagePath, _hostingEnvironment);
                GameMappingUtilities.UpdateGameFromViewModel(game, model);

                if (model.ImageFile != null) // Handle image update
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
            return await _imageService.SaveImageAsync(imageFile, "game");
        }

    }
}
