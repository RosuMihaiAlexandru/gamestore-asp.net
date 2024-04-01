using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Response;
using Gamesmarket.Domain.ViewModel.Game;
using Gamesmarket.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamesmarket.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("getGames")]
        public async Task<IActionResult> GetGames()
        {
            var response = await _gameService.GetGames(); //Get the list of games from the service
			if (response.StatusCode == Domain.Enum.StatusCode.OK) //If the operation was successful
			{
                return Ok(response.Data.ToList()); //Return the list of games
			}
            else
            {
                // Cast to BaseResponse<IEnumerable<Game>> to access Description
                var concreteResponse = (BaseResponse<IEnumerable<Game>>)response;
                return BadRequest(concreteResponse.Description);
            }
        }

        [HttpGet("getGame/{id}")]
        public async Task<IActionResult> GetGame(int id)
        {
            var response = await _gameService.GetGame(id); //Get a single game by its id from the service
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(response.Data);
            }
            else
            {
                // Cast to BaseResponse<Game> for specific error handling
                var concreteResponse = (BaseResponse<Game>)response;
                return BadRequest(concreteResponse.Description);
            }
        }

        [Authorize("StaffPolicy")]
        [HttpDelete("delete/{id}")]
		public async Task<IActionResult> Delete (int id)
        {
            var response = await _gameService.DeleteGame(id); //Delete a game by its id using the service
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
                return Ok(new { description = "Game deleted successfully." });
            }
            else
            {
                // Cast to BaseResponse<bool> for specific error handling
                var concreteResponse = (BaseResponse<bool>)response;
                return BadRequest(concreteResponse.Description);
            }
        }

        [Authorize("StaffPolicy")]
        [HttpPost("createGame")]
        public async Task<IActionResult> CreateGame([FromForm] GameViewModel model)
        {
            var response = await _gameService.CreateGame(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(new { data = response.Data, description = "Game created successfully." });
            }
            else
            {
                // Access the Description property of the BaseResponse<GameViewModel>
                var concreteResponse = (BaseResponse<GameViewModel>)response;
                return BadRequest(concreteResponse.Description);
            }
        }

        [Authorize("StaffPolicy")]
        [HttpPatch("editGame/{id}")]
        public async Task<IActionResult> EditGame(int id, [FromForm] GameViewModel model)
        {
            var response = await _gameService.Edit(id, model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(new { data = response.Data, description = "Game edited successfully." });
            }
            else
            {
                // Cast to BaseResponse<Game> for specific error handling
                var concreteResponse = (BaseResponse<Game>)response;
                return BadRequest(concreteResponse.Description);
            }
        }

        [HttpGet("findGamesByNameOrDev/{searchQuery}")]
        public async Task<IActionResult> FindGamesByNameOrDev(string searchQuery)
        {
            var response = await _gameService.SearchGames(searchQuery);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(response.Data);
            }
            else
            {
                // Cast to BaseResponse<IEnumerable<Game>> to access Description
                var concreteResponse = (BaseResponse<IEnumerable<Game>>)response;
                return BadRequest(concreteResponse.Description);
            }        
        }
    }
}
