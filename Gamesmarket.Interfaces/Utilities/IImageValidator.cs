using Gamesmarket.Domain.Response;
using Microsoft.AspNetCore.Http;

namespace Gamesmarket.Interfaces.Utilities
{
    public interface IImageValidator
    {
        Task<IBaseResponse<string>> ValidateImageAsync(IFormFile imageFile);
    }
}
