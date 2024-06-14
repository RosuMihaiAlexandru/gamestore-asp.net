using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.ViewModel.Order;
using System.Collections.Generic;
using System.Linq;

namespace Gamesmarket.Utilities.Cart
{
    public static class CartQueriesUtilities
    {
        public static IEnumerable<OrderViewModel> MapOrdersToViewModels(IEnumerable<Order> orders, IEnumerable<Game> games)
        {
            return from p in orders
                   join g in games on p.GameId equals g.Id
                   select new OrderViewModel
                   {
                       Id = p.Id,
                       GameName = g.Name,
                       GameDeveloper = g.Developer,
                       GameGenre = g.GameGenre.ToString(),
                       GamePrice = g.Price.ToString(),
                       ImagePath = g.ImagePath
                   };
        }

        public static OrderViewModel MapOrderToViewModel(Order order, Game game)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                GameName = game.Name,
                GameDeveloper = game.Developer,
                GameGenre = game.GameGenre.ToString(),
                GamePrice = game.Price.ToString(),
                Email = order.Email,
                Name = order.Name,
                DateCreate = order.DateCreated.ToLongDateString()
            };
        }
    }
}
