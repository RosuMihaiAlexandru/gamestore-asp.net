using Microsoft.AspNetCore.Http;

namespace Gamesmarket.Interfaces.Utilities
{
    public interface IFileManager
    {
        Task<string> SaveFileAsync(IFormFile file, string subfolder);
    }
}
