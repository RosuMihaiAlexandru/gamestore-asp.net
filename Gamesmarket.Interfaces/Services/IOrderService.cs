using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Response;
using Gamesmarket.Domain.ViewModel.Order;

namespace Gamesmarket.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IBaseResponse<Order>> Create(CreateOrderViewModel model);

        Task<IBaseResponse<bool>> Delete(long id);
    }
}