using Gamesmarket.Domain.Response;
using Gamesmarket.Domain.ViewModel.Order;

namespace Gamesmarket.Service.Interfaces
{
    public interface ICartService
    {
        Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetItems(string userName);

        Task<IBaseResponse<OrderViewModel>> GetItem(string userName, long id);
    }
}