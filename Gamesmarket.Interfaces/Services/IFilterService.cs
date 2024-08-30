using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;

namespace Gamesmarket.Interfaces.Services
{
    public interface IFilterService
    {
        Task<IBaseResponse<IEnumerable<Game>>> SearchGames(string searchQuery);
        Task<IBaseResponse<IEnumerable<Game>>> GetGamesByGenre(GameGenre genre);
    }
}
