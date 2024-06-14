using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.ViewModel.Game;

namespace Gamesmarket.Utilities.Games
{
    public static class GameMappingUtilities
    {
        public static Game CreateGameFromViewModel(GameViewModel gameViewModel, string imagePath)
        {
            return new Game
            {
                Description = gameViewModel.Description,
                ReleaseDate = gameViewModel.ReleaseDate,
                Developer = gameViewModel.Developer,
                Price = gameViewModel.Price,
                Name = gameViewModel.Name,
                GameGenre = (GameGenre)Convert.ToInt32(gameViewModel.GameGenre),
                ImagePath = imagePath,
            };
        }

        public static GameViewModel CreateViewModelFromGame(Game game)
        {
            return new GameViewModel
            {
                Id = game.Id,
                Description = game.Description,
                ReleaseDate = game.ReleaseDate,
                Developer = game.Developer,
                Price = game.Price,
                Name = game.Name,
                GameGenre = ((int)game.GameGenre).ToString(),
            };
        }

        public static void UpdateGameFromViewModel(Game game, GameViewModel model)
        {
            game.Description = model.Description;
            game.Developer = model.Developer;
            game.ReleaseDate = model.ReleaseDate;
            game.Price = model.Price;
            game.Name = model.Name;
            game.GameGenre = (GameGenre)Convert.ToInt32(model.GameGenre);
        }
    }
}
