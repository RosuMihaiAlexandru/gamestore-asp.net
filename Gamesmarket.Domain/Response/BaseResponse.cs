using Gamesmarket.Domain.Enum;

namespace Gamesmarket.Domain.Response
{//A class and interface designed to represent responses from services
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }

        public StatusCode StatusCode { get; set; }

        public T Data { get; set; }
    }

    public interface IBaseResponse<T>
    {
        StatusCode StatusCode { get; }
        T Data { get; set; }
    }
}
