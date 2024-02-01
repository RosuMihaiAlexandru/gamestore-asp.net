using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;
using Gamesmarket.Domain.ViewModel.Order;
using Gamesmarket.Service.Interfaces;
using GamesMarket.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gamesmarket.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly UserManager<User> _userManager;
        private readonly IBaseRepository<Order> _orderRepository;
        public OrderService(UserManager<User> userManager, IBaseRepository<Order> orderRepository)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
        }
        public async Task<IBaseResponse<Order>> Create(CreateOrderViewModel model)
        {
            try
            {
                // Find the user by username
                var user = await _userManager
                    .Users
                    .Include(x => x.Cart)
                    .FirstOrDefaultAsync(x => x.UserName == model.Login);

                if (user == null)
                {
                    return new BaseResponse<Order>()
                    {
                        Description = "User is not found",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                // Create a new order
                var order = new Order()
                {
                    Email = model.Email,
                    Name = model.Name,
                    DateCreated = DateTime.Now,
                    CartId = user.Cart.Id,
                    GameId = model.GameId
                };

                // Save the order to the repository
                await _orderRepository.Create(order);

                return new BaseResponse<Order>()
                {
                    Description = "Order created",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(long id)
        {
            try
            {
                // Find the order by id and include related cart
                var order = _orderRepository.GetAll()
                    .Include(x => x.Cart)
                    .FirstOrDefault(x => x.Id == id);

                if (order == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.OrderNotFound,
                        Description = "Order not found"
                    };
                }

                // Delete the order from the repository
                await _orderRepository.Delete(order);
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.OK,
                    Description = "Order deleted"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
