using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;
using Gamesmarket.Domain.ViewModel.Game;
using Gamesmarket.Service.Interfaces;
using GamesMarket.DAL.Interfaces;
using System.ComponentModel;

namespace Gamesmarket.Service.Implementations
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        //DI of IGameRepository in GameService class constructor
        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
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
					baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
				//If the game is found, assign it to the response data
				baseResponse.Data = game;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Game>();
                {
                    baseResponse.Description = $"[GetGame] : {ex.Message}";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                };
            }
        }

        // Method for createing game  
        public async Task<IBaseResponse<GameViewModel>> CreateGame(GameViewModel gameViewModel)
        {
            var baseResponse = new BaseResponse<GameViewModel>();
            try
            {
                var game = new Game()
				{//Create a new Game object based on the GameViewModel
					Description = gameViewModel.Description,
                    ReleaseDate = DateTime.Now,
                    Developer = gameViewModel.Developer,
                    Price = gameViewModel.Price,
                    Name = gameViewModel.Name,
                    GameGenre = (GameGenre)Convert.ToInt32(gameViewModel.GameGenre)
                };
                await _gameRepository.Create(game); //Call repository method to create the game
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
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var game = await _gameRepository.Get(id);
                if (game == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
				// Call method to delete the game
				await _gameRepository.Delete(game);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>();
                {
                    baseResponse.Description = $"[DeleteGame] : {ex.Message}";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                };
            }
        }

        // Method for getting one game by name
        public async Task<IBaseResponse<Game>> GetGameByName(string name)
        {
            var baseResponse = new BaseResponse<Game>();
            try
            {
                var game = await _gameRepository.GetByName(name);//Get a game by its name 
				if (game == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = game;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Game>();
                {
                    baseResponse.Description = $"[GetGameByName] : {ex.Message}";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
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
                    baseResponse.StatusCode = StatusCode.OK;

                    return baseResponse;
                }
				//If games are found, assign the list to the response data
				baseResponse.Data = games;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<IEnumerable<Game>>();
                {
                    baseResponse.Description = $"[GetGames] : {ex.Message}";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
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
				//Updated game properties using data from GameViewModel
				game.Description = model.Description;
                game.Developer = model.Developer;
                game.ReleaseDate = model.ReleaseDate;
				game.Price = model.Price;

				// Call the repository method to update the game in db
				await _gameRepository.Update(game);
				return baseResponse;

				//GameGenre

			}
			catch (Exception ex)
			{
				return new BaseResponse<Game>();
				{
					baseResponse.Description = $"[Edit] : {ex.Message}";
					baseResponse.StatusCode = StatusCode.InternalServerError;
				};
			}
		}
	}
}
