using Gamesmarket.Domain.Entity;

namespace GamesMarket.DAL.Interfaces
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        Task<Game> GetByName(string name);
    }
}
