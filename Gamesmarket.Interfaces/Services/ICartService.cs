using Gamesmarket.Domain.Response;
using Gamesmarket.Domain.ViewModel.Order;

namespace Gamesmarket.Interfaces.Services
{
    public interface ICartService
    {
        Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetItems(string userName);

        Task<IBaseResponse<OrderViewModel>> GetItem(string userName, long id);
    }
}