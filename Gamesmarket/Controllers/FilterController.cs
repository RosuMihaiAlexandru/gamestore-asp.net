using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;
using Gamesmarket.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gamesmarket.Controllers
{
    [ApiController]
    [Route("api/filter")]
    public class FilterController : ControllerBase
    {
        private readonly IFilterService _filtereService;

        public FilterController(IFilterService filterService)
        {
            _filtereService = filterService;
        }

        [HttpGet("findGamesByNameOrDev/{searchQuery}")]
        public async Task<IActionResult> FindGamesByNameOrDev(string searchQuery)
        {
            var response = await _filtereService.SearchGames(searchQuery);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(response.Data);
            }
            else
            {
                var concreteResponse = (BaseResponse<IEnumerable<Game>>)response;
                return BadRequest(concreteResponse.Description);
            }
        }

        [HttpGet("getGamesByGenre/{genre}")]
        public async Task<IActionResult> GetGamesByGenre(GameGenre genre)
        {
            var response = await _filtereService.GetGamesByGenre(genre);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(response.Data);
            }
            else
            {
                var concreteResponse = (BaseResponse<IEnumerable<Game>>)response;
                return BadRequest(concreteResponse.Description);
            }
        }

    }
}
