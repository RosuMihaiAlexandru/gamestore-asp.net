using Gamesmarket.Domain.ViewModel.Game;
using Gamesmarket.Service.Interfaces;
using GamesMarket.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamesmarket.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("GetGames")]
        public async Task<IActionResult> GetGames()
        {
            var response = await _gameService.GetGames(); //Get the list of games from the service
			if (response.StatusCode == Domain.Enum.StatusCode.OK) //If the operation was successful
			{
                return Ok(response.Data.ToList()); //Return the view with the list of games
			}
            return BadRequest("Operation was not successful");  //Redirect to error page if operation was not successful
        }

        [HttpGet("GetGame/{id}")]
        public async Task<IActionResult> GetGame(int id)
        {
            var responce = await _gameService.GetGame(id); //Get a single game by its id from the service
			if (responce.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(responce.Data);
            }
            return BadRequest("Operation was not successful");
        }

        [HttpDelete("Delete/{id}")]
		public async Task<IActionResult> Delete (int id)
        {
            var responce = await _gameService.DeleteGame(id); //Delete a game by its id using the service
			if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			{
                return Ok(responce.Data);
            }
            return BadRequest("Operation was not successful");
        }

        [HttpPost("CreateGame")]
        public async Task<IActionResult> CreateGame(GameViewModel model)
        {
            if (ModelState.IsValid)//Operation must satisfyer properties of GameViewModel attributes
            {
                var response = await _gameService.CreateGame(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Ok(response);
                }
                return BadRequest("Operation was not successful");
            }
            return BadRequest(ModelState);
        }

        [HttpPatch("EditGame/{id}")]
        public async Task<IActionResult> EditGame(int id, GameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _gameService.Edit(id, model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Ok(response);
                }
                return BadRequest("Operation was not successful");
            }

            return BadRequest(ModelState);
        }
    }
}
