using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Response;

namespace Gamesmarket.Interfaces.Services
{
    public interface ISortService
    {
        Task<IBaseResponse<IEnumerable<Game>>> GetGamesByIdDesc();
        Task<IBaseResponse<IEnumerable<Game>>> GetGamesByReleaseDate(bool ascending);
        Task<IBaseResponse<IEnumerable<Game>>> GetGamesByPrice(bool ascending);
    }
}
