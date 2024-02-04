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
            return BadRequest("Operation was not successful");  //Redirect to error page if operation was not successful
        }

        [HttpGet("getGame/{id}")]
        public async Task<IActionResult> GetGame(int id)
        {
            var response = await _gameService.GetGame(id); //Get a single game by its id from the service
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(response.Data);
            }
            return BadRequest("Operation was not successful");
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
		public async Task<IActionResult> Delete (int id)
        {
            var response = await _gameService.DeleteGame(id); //Delete a game by its id using the service
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
                return Ok(new { description = "Game deleted successfully." });
            }
            return BadRequest("Operation was not successful");
        }

        [HttpPost("createGame")]
        public async Task<IActionResult> CreateGame([FromForm] GameViewModel model)
        {
            var response = await _gameService.CreateGame(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(new { data = response.Data, description = "Game created successfully." });
            }
            return BadRequest("Operation was not successful");
        }

        [HttpPatch("editGame/{id}")]
        public async Task<IActionResult> EditGame(int id, [FromForm] GameViewModel model)
        {
            var response = await _gameService.Edit(id, model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(new { data = response.Data, description = "Game edited successfully." });
            }
            return BadRequest("Operation was not successful");
        }

        [HttpGet("findGamesByNameOrDev/{searchQuery}")]
        public async Task<IActionResult> FindGamesByNameOrDev(string searchQuery)
        {
            var response = await _gameService.SearchGames(searchQuery);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(response.Data);
            }
            return BadRequest("Game not found");
        }
    }
}
