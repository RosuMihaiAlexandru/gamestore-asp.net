using Gamesmarket.Domain.Response;
using Microsoft.AspNetCore.Http;

namespace Gamesmarket.Service.Interfaces
{
    public interface IImageService
    {
        Task<IBaseResponse<string>> SaveImageAsync(IFormFile imageFile, string subfolder);
    }
}
