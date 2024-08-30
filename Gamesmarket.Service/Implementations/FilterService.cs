using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;
using Gamesmarket.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Gamesmarket.Interfaces.Services;

namespace Gamesmarket.Service.Implementations
{
    public class FilterService : IFilterService
    {
        private readonly IBaseRepository<Game> _gameRepository;

        public FilterService(IBaseRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        // Get games by its name or developer
        public async Task<IBaseResponse<IEnumerable<Game>>> SearchGames(string searchQuery)
        {
            var baseResponse = new BaseResponse<IEnumerable<Game>>();
            try
            {
                var games = await _gameRepository.GetAll()
                    .Where(g => g.Name == searchQuery || g.Developer == searchQuery)
                    .ToListAsync();
                if (games == null || !games.Any())
                {
                    baseResponse.Description = "Games not found";
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

        // Get games by genre
        public async Task<IBaseResponse<IEnumerable<Game>>> GetGamesByGenre(GameGenre genre)
        {
            var baseResponse = new BaseResponse<IEnumerable<Game>>();
            try
            {
                var games = await _gameRepository.GetAll()
                                .Where(g => g.GameGenre == genre)
                                .ToListAsync();
                if (games == null || !games.Any())
                {
                    baseResponse.Description = "Games not found";
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
                    Description = $"[GetGamesByGenre] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
