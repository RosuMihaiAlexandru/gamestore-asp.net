using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;
using Gamesmarket.Interfaces.Utilities;
using Microsoft.AspNetCore.Http;

namespace Gamesmarket.Utilities.Image
{
    public class ImageValidator : IImageValidator
    {
        public async Task<IBaseResponse<string>> ValidateImageAsync(IFormFile imageFile)
        {
            var baseResponse = new BaseResponse<string>();

            if (imageFile == null || imageFile.Length <= 0)
            {
                baseResponse.Description = "Invalid image file.";
                baseResponse.StatusCode = StatusCode.InvalidData;
                return baseResponse;
            }

            // Validate image format
            var allowedFormats = new[] { "image/jpeg", "image/png", "image/webp" };
            if (!allowedFormats.Contains(imageFile.ContentType.ToLower()))
            {
                baseResponse.Description = "Invalid image format. Supported formats: JPEG, PNG, WebP.";
                baseResponse.StatusCode = StatusCode.InvalidData;
                return baseResponse;
            }

            // Validate image size
            var maxFileSizeInBytes = 2 * 1024 * 1024; // 2 MB
            if (imageFile.Length > maxFileSizeInBytes)
            {
                baseResponse.Description = "Image size exceeds the maximum allowed size (2 MB).";
                baseResponse.StatusCode = StatusCode.InvalidData;
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCode.OK;
            return baseResponse;
        }
    }
}
