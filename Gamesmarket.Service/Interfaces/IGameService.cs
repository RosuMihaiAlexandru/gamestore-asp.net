using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Response;
using Gamesmarket.Domain.ViewModel.Game;

namespace Gamesmarket.Service.Interfaces
{
    public interface IGameService
    { //An interface for implementing various services that provide functions for working with games db.
        Task<IBaseResponse<IEnumerable<Game>>> GetGames();

        Task<IBaseResponse<Game>> GetGame(int id);

        Task<IBaseResponse<GameViewModel>> CreateGame(GameViewModel gameViewModel);

        Task<IBaseResponse<bool>> DeleteGame(int id);

        Task<IBaseResponse<Game>> GetGameByName(string name);

		Task<IBaseResponse<Game>> Edit(int id, GameViewModel model);
	}
}
