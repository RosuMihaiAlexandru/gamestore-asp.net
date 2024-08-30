using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;
using Gamesmarket.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Gamesmarket.Interfaces.Services;

namespace Gamesmarket.Service.Implementations
{
    public class SortService : ISortService
    {
        private readonly IBaseRepository<Game> _gameRepository;

        public SortService(IBaseRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        // Get games by descending Id
        public async Task<IBaseResponse<IEnumerable<Game>>> GetGamesByIdDesc()
        {
            var baseResponse = new BaseResponse<IEnumerable<Game>>();
            try
            {
                var games = await _gameRepository.GetAll()
                                .OrderByDescending(g => g.Id)
                                .ToListAsync();
                baseResponse.Data = games;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Game>>
                {
                    Description = $"[GetGamesByIdDesc] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        // Games by release date, ascending or descending
        public async Task<IBaseResponse<IEnumerable<Game>>> GetGamesByReleaseDate(bool ascending)
        {
            var baseResponse = new BaseResponse<IEnumerable<Game>>();
            try
            {
                var gamesQuery = _gameRepository.GetAll();

                var games = ascending
                    ? await gamesQuery.OrderBy(g => g.ReleaseDate).ToListAsync()
                    : await gamesQuery.OrderByDescending(g => g.ReleaseDate).ToListAsync();

                baseResponse.Data = games;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Game>>
                {
                    Description = $"[GetGamesByReleaseDate] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        // Games by price, ascending or descending
        public async Task<IBaseResponse<IEnumerable<Game>>> GetGamesByPrice(bool ascending)
        {
            var baseResponse = new BaseResponse<IEnumerable<Game>>();
            try
            {
                var gamesQuery = _gameRepository.GetAll();

                var games = ascending
                    ? await gamesQuery.OrderBy(g => g.Price).ToListAsync()
                    : await gamesQuery.OrderByDescending(g => g.Price).ToListAsync();

                baseResponse.Data = games;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Game>>
                {
                    Description = $"[GetGamesByPrice] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
