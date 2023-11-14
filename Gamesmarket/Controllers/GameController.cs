using Gamesmarket.Domain.ViewModel.Game;
using Gamesmarket.Service.Interfaces;
using GamesMarket.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamesmarket.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            var response = await _gameService.GetGames(); //Get the list of games from the service
			if (response.StatusCode == Domain.Enum.StatusCode.OK) //If the operation was successful
			{
                return View(response.Data.ToList()); //Return the view with the list of games
			}
            return RedirectToAction("Error"); //Redirect to error page if operation was not successful
		}

        [HttpGet]
        public async Task<IActionResult> GetGame(int id)
        {
            var responce = await _gameService.GetGame(id); //Get a single game by its id from the service
			if (responce.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(responce.Data);
            }
            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete (int id)
        {
            var responce = await _gameService.DeleteGame(id); //Delete a game by its id using the service
			if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return RedirectToAction("GetGames");
			}
			return RedirectToAction("Error");
		}   

        [HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Save(int id)
        {
            if(id == 0)
			{// If id is 0, it means adding a new object, so return the view for adding
				return View();
            }
            var responce = await _gameService.GetGame(id);//Get a game by its id for editing using the service
			if (responce.StatusCode == Domain.Enum.StatusCode.OK)
			{// Return the view for editing with the data of existing game
				return View(responce.Data);
			}
			return RedirectToAction("Error");
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Save(GameViewModel model)
        {
            if(ModelState.IsValid)//Operation must satisfyer properties of GameViewModel attributes
			{
                if(model.Id == 0)//We add new object to db
				{
                   await _gameService.CreateGame(model);
                }
				else//We updating an existing object in db
				{
                    await _gameService.Edit(model.Id, model);
                }
            }
			//Redirect to the GetGames action after the operation
			return RedirectToAction("GetGames");
        }
	}
}
