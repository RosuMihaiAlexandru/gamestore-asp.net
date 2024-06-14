using Microsoft.AspNetCore.Hosting;

namespace Gamesmarket.Utilities.Games
{
    public static class FileUtilities
    {
        public static void DeleteImageFile(string imagePath, IWebHostEnvironment hostingEnvironment)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                try
                {
                    Console.WriteLine("Deleting image file...");
                    string fullPath = Path.Combine(hostingEnvironment.WebRootPath, imagePath);
                    File.Delete(fullPath);
                    Console.WriteLine("Image file deleted successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting image file: {ex.Message}");
                }
            }
        }
    }
}
