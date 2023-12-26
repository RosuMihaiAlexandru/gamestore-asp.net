using Gamesmarket.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Gamesmarket.Service.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ImageService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile, string subfolder)
        {
            if (imageFile == null || imageFile.Length <= 0)
            {
                throw new InvalidOperationException("Invalid image file.");
            }

            // Validate image format
            var allowedFormats = new[] { "image/jpeg", "image/png", "image/webp" };
            if (!allowedFormats.Contains(imageFile.ContentType.ToLower()))
            {
                throw new InvalidOperationException("Invalid image format. Supported formats: JPEG, PNG, WebP.");
            }

            // Validate image size
            var maxFileSizeInBytes = 2 * 1024 * 1024; // 2 MB
            if (imageFile.Length > maxFileSizeInBytes)
            {
                throw new InvalidOperationException("Image size exceeds the maximum allowed size (2 MB).");
            }

            // Create a unique file name to avoid overwriting existing files
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";

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
                await imageFile.CopyToAsync(stream);
            }

            // Return the relative path (from wwwroot) to the saved image
            var relativePath = Path.Combine("images", subfolder, fileName);

            // Normalize the path to replace backslashes with forward slashes
            return relativePath.Replace('\\', '/');
        }
    }
}
