using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Response;
using Gamesmarket.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gamesmarket.Controllers
{
    [ApiController]
    [Route("api/sort")]
    public class SortController : ControllerBase
    {
        private readonly ISortService _sortService;

        public SortController(ISortService sortService)
        {
            _sortService = sortService;
        }

        [HttpGet("getGamesByIdDesc")]
        public async Task<IActionResult> GetGamesByIdDesc()
        {
            var response = await _sortService.GetGamesByIdDesc();
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

        [HttpGet("getGamesByReleaseDate/{ascending}")]
        public async Task<IActionResult> GetGamesByReleaseDate(bool ascending)
        {
            var response = await _sortService.GetGamesByReleaseDate(ascending);
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

        [HttpGet("getGamesByPrice/{ascending}")]
        public async Task<IActionResult> GetGamesByPrice(bool ascending)
        {
            var response = await _sortService.GetGamesByPrice(ascending);
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
