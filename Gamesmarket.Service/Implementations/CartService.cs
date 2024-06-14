using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;
using Gamesmarket.Domain.ViewModel.Order;
using Gamesmarket.Interfaces.Services;
using Gamesmarket.DAL.Interfaces;
using Gamesmarket.Utilities.Cart;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gamesmarket.Service.Implementations
{
    public class CartService : ICartService
    {
        private readonly UserManager<User> _userManager;
        private readonly IBaseRepository<Game> _gameRepository;

        public CartService (UserManager<User> userManager, IBaseRepository<Game> gameRepository)
        {
            _userManager = userManager;
            _gameRepository = gameRepository;
        }

        public async Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetItems(string userName)
        {
            try
            {
                var user = await _userManager // Find the user by username
                    .Users
                    .Include(x => x.Cart)
                        .ThenInclude(x => x.Orders)
                    .FirstOrDefaultAsync(x => x.UserName == userName);

                if (user == null)
                {
                    return new BaseResponse<IEnumerable<OrderViewModel>>()
                    {
                        Description = "User is not found",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var orders = user.Cart?.Orders; // Retrieve orders related to the user's cart
                if (orders == null || !orders.Any())
                {
                    return new BaseResponse<IEnumerable<OrderViewModel>>()
                    {
                        Description = "No orders found for the user.",
                        StatusCode = StatusCode.OrderNotFound
                    };
                }

                // Map orders to view models
                var games = _gameRepository.GetAll();
                var response = CartQueriesUtilities.MapOrdersToViewModels(orders, games);

                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<OrderViewModel>> GetItem(string userName, long id)
        {
            try
            {
                var user = await _userManager.Users // Find the user by username
                    .Include(x => x.Cart)
                        .ThenInclude(x => x.Orders)
                    .FirstOrDefaultAsync(x => x.Email == userName);

                if (user == null)
                {
                    return new BaseResponse<OrderViewModel>()
                    {
                        Description = "User is not found",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                // Retrieve orders related to the user's cart and filter by id
                var orders = user.Cart?.Orders.Where(x => x.Id == id).ToList();
                if (orders == null || orders.Count == 0)
                {
                    return new BaseResponse<OrderViewModel>()
                    {
                        Description = "No orders",
                        StatusCode = StatusCode.OrderNotFound
                    };
                }
                // Map order to view model
                var game = _gameRepository.GetAll().FirstOrDefault(g => g.Id == orders.First().GameId);
                var response = CartQueriesUtilities.MapOrderToViewModel(orders.First(), game);

                return new BaseResponse<OrderViewModel>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderViewModel>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
