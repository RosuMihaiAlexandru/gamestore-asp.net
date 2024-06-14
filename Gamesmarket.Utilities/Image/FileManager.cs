using Gamesmarket.Interfaces.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Gamesmarket.Utilities.Image
{
    public class FileManager : IFileManager
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileManager(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string subfolder)
        {
            // Create a unique file name to avoid overwriting existing files
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";

            // Combine the web root path, subfolder, and file name to get the full path
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", subfolder, fileName);

            // Create the directory if it doesn't exist
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Save the file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the relative path (from wwwroot) to the saved image and normalize the path
            return Path.Combine("images", subfolder, fileName).Replace('\\', '/');
        }
    }
}
