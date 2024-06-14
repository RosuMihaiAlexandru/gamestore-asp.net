using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Response;
using Gamesmarket.Domain.ViewModel.Game;
using Microsoft.AspNetCore.Http;

namespace Gamesmarket.Interfaces.Services
{
    public interface IGameService
    { //An interface for implementing various services that provide functions for working with games db.
        Task<IBaseResponse<IEnumerable<Game>>> GetGames();

        Task<IBaseResponse<Game>> GetGame(int id);

        Task<IBaseResponse<GameViewModel>> CreateGame(GameViewModel gameViewModel);

        Task<IBaseResponse<bool>> DeleteGame(int id);

        Task<IBaseResponse<IEnumerable<Game>>> SearchGames(string searchQuery);

		Task<IBaseResponse<Game>> Edit(int id, GameViewModel model);

        Task<IBaseResponse<string>> SaveGameImage(IFormFile imageFile);
    }
}
