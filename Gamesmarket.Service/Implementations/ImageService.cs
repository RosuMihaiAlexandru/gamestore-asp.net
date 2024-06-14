using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;
using Gamesmarket.Interfaces.Services;
using Gamesmarket.Interfaces.Utilities;
using Microsoft.AspNetCore.Http;

namespace Gamesmarket.Service.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IImageValidator _imageValidator;
        private readonly IFileManager _fileManager;

        public ImageService(IImageValidator imageValidator, IFileManager fileManager)
        {
            _imageValidator = imageValidator;
            _fileManager = fileManager;
        }

        public async Task<IBaseResponse<string>> SaveImageAsync(IFormFile imageFile, string subfolder)
        {
            var validationResponse = await _imageValidator.ValidateImageAsync(imageFile);
            if (validationResponse.StatusCode != StatusCode.OK)
            {
                return validationResponse;
            }

            var relativePath = await _fileManager.SaveFileAsync(imageFile, subfolder);
            return new BaseResponse<string>
            {
                Data = relativePath,
                StatusCode = StatusCode.OK
            };
        }
    }
}
