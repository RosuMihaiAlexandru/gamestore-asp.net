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
                       GameId = g.Id,
                       GameName = g.Name,
                       GamePrice = g.Price,
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
                GamePrice = game.Price,
                Email = order.Email,
                Name = order.Name,
                DateCreate = order.DateCreated.ToShortDateString()
            };
        }
    }
}
