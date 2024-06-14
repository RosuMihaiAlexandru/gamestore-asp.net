using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;
using Gamesmarket.Domain.ViewModel.Order;
using Gamesmarket.Interfaces.Services;
using Gamesmarket.DAL.Interfaces;
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
                var user = await _userManager // Find the user by username
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

                var order = new Order()
                {
                    Email = model.Email,
                    Name = model.Name,
                    DateCreated = DateTime.Now,
                    CartId = user.Cart.Id,
                    GameId = model.GameId
                };

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
                var order = _orderRepository.GetAll() // Find the order by id and include related cart
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
