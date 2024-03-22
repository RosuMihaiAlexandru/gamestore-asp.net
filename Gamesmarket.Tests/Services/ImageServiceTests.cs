using Gamesmarket.Service.Implementations;
using Gamesmarket.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Hosting;
using FluentAssertions;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;

namespace Gamesmarket.Tests.Services
{
    public class ImageServiceTests
    {
        private readonly Mock<IWebHostEnvironment> _mockHostingEnvironment;
        private readonly IImageService _imageService;

        public ImageServiceTests()
        {
            _mockHostingEnvironment = new Mock<IWebHostEnvironment>();
            _imageService = new ImageService(_mockHostingEnvironment.Object);
        }

        [Fact]
        public async Task SaveImageAsync_ValidImage_Success()
        {
            // Arrange
            string currentDirectory = Directory.GetCurrentDirectory();
            string testImagePath = Path.Combine(currentDirectory, "..", "..", "..", "TestPhotos", "testpic.jpg");
            using var fileStream = File.OpenRead(testImagePath);
            var mockImageFile = new Mock<IFormFile>();
            mockImageFile.Setup(f => f.OpenReadStream()).Returns(fileStream);
            mockImageFile.Setup(f => f.FileName).Returns("testpic.jpg");
            mockImageFile.Setup(f => f.ContentType).Returns("image/jpeg");
            mockImageFile.Setup(f => f.Length).Returns(fileStream.Length);

            string subfolder = "game";

            string mockWebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"); // Set up mock WebRootPath
            _mockHostingEnvironment.Setup(h => h.WebRootPath).Returns(mockWebRootPath);

            // Act
            var response = await _imageService.SaveImageAsync(mockImageFile.Object, subfolder);
            string savedImagePath = response.Data; // Extract the saved path from the response

            // Check if the saved path starts with the expected path and contains the filename
            string expectedPath = Path.Combine("images", subfolder).Replace('\\', '/');
            string actualPath = savedImagePath.Replace('\\', '/');
            string fullSavedPath = Path.Combine(mockWebRootPath, savedImagePath);

            // Assert
            savedImagePath.Should().NotBeNullOrEmpty();
            actualPath.Should().StartWith(expectedPath); // Check if the path starts with the expected prefix
            File.Exists(fullSavedPath).Should().BeTrue(); // Verify that the file was actually saved
        }

        [Fact]
        public async Task SaveImageAsync_InvalidImageFormat_ThrowsException()
        {
            // Arrange
            string currentDirectory = Directory.GetCurrentDirectory();
            string testImagePath = Path.Combine(currentDirectory, "..", "..", "..", "TestPhotos", "wrongformat.gif");
            using var fileStream = File.OpenRead(testImagePath);
            var mockImageFile = new Mock<IFormFile>();
            mockImageFile.Setup(f => f.OpenReadStream()).Returns(fileStream);
            mockImageFile.Setup(f => f.FileName).Returns("wrongformat.gif");
            mockImageFile.Setup(f => f.ContentType).Returns("image/gif"); // Set the content type to GIF
            mockImageFile.Setup(f => f.Length).Returns(fileStream.Length);

            string subfolder = "game";

            // Act
            var response = await _imageService.SaveImageAsync(mockImageFile.Object, subfolder);
            var concreteResponse = (BaseResponse<string>)response;

            // Assert
            concreteResponse.StatusCode.Should().Be(StatusCode.InvalidData);
            concreteResponse.Description.Should().Contain("Invalid image format. Supported formats: JPEG, PNG, WebP.");
        }

        [Fact]
        public async Task SaveImageAsync_ImageSizeExceedsLimit_ThrowsException()
        {
            // Arrange
            string currentDirectory = Directory.GetCurrentDirectory();
            string testImagePath = Path.Combine(currentDirectory, "..", "..", "..", "TestPhotos", "3mb.jpg");
            using var fileStream = File.OpenRead(testImagePath);
            var mockImageFile = new Mock<IFormFile>();
            mockImageFile.Setup(f => f.OpenReadStream()).Returns(fileStream);
            mockImageFile.Setup(f => f.FileName).Returns("3mb.jpg");
            mockImageFile.Setup(f => f.ContentType).Returns("image/jpeg");
            mockImageFile.Setup(f => f.Length).Returns(fileStream.Length);

            string subfolder = "game";

            // Act
            var response = await _imageService.SaveImageAsync(mockImageFile.Object, subfolder);
            var concreteResponse = (BaseResponse<string>)response;

            // Assert
            concreteResponse.StatusCode.Should().Be(StatusCode.InvalidData);
            concreteResponse.Description.Should().Contain("Image size exceeds the maximum allowed size (2 MB).");
        }

        [Fact]
        public async Task SaveImageAsync_NullImageFile_ThrowsException()
        {
            // Arrange
            string subfolder = "game";

            // Act
            var response = await _imageService.SaveImageAsync(null, subfolder);
            var concreteResponse = (BaseResponse<string>)response;

            // Assert
            concreteResponse.StatusCode.Should().Be(StatusCode.InvalidData);
            concreteResponse.Description.Should().Contain("Invalid image file.");
        }
    }
}