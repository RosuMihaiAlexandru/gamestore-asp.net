using Microsoft.AspNetCore.Http;

namespace Gamesmarket.Service.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile, string subfolder);
    }
}
