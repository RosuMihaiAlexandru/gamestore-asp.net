using Gamesmarket.Interfaces.Services;
using Gamesmarket.Interfaces.Utilities;
using Gamesmarket.Service.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Gamesmarket.Tests.Services.Image.Base
{
    public abstract class ImageServiceTestBase
    {
        protected readonly Mock<IWebHostEnvironment> _mockHostingEnvironment;
        protected readonly Mock<IImageValidator> _mockImageValidator;
        protected readonly Mock<IFileManager> _mockFileManager;
        protected readonly IImageService _imageService;

        protected ImageServiceTestBase()
        {
            _mockHostingEnvironment = new Mock<IWebHostEnvironment>();
            _mockImageValidator = new Mock<IImageValidator>();
            _mockFileManager = new Mock<IFileManager>();
            _imageService = new ImageService(_mockImageValidator.Object, _mockFileManager.Object);
        }

        protected Mock<IFormFile> CreateMockImageFile(string fileName, string contentType, long length, Stream stream)
        {
            var mockImageFile = new Mock<IFormFile>();
            mockImageFile.Setup(f => f.OpenReadStream()).Returns(stream);
            mockImageFile.Setup(f => f.FileName).Returns(fileName);
            mockImageFile.Setup(f => f.ContentType).Returns(contentType);
            mockImageFile.Setup(f => f.Length).Returns(length);
            return mockImageFile;
        }
    }
}
