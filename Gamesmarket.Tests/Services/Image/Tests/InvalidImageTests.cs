using FluentAssertions;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;
using Gamesmarket.Tests.Services.Image.Base;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Gamesmarket.Tests.Services.Image.Tests
{
    public class InvalidImageTests : ImageServiceTestBase
    {
        [Fact]
        public async Task SaveImageAsync_InvalidImageFormat_ThrowsException()
        {
            // Arrange
            // Set up an invalid format test image file
            string currentDirectory = Directory.GetCurrentDirectory();
            string testImagePath = Path.Combine(currentDirectory, "..", "..", "..", "TestPhotos", "wrongformat.gif");
            using var fileStream = File.OpenRead(testImagePath);
            var mockImageFile = CreateMockImageFile("wrongformat.gif", "image/gif", fileStream.Length, fileStream); // Set the content type to GIF

            string subfolder = "game";
            var validationResponse = new BaseResponse<string> // Set up the image validator to return an invalid format response
            {
                StatusCode = StatusCode.InvalidData,
                Description = "Invalid image format. Supported formats: JPEG, PNG, WebP."
            };
            _mockImageValidator.Setup(v => v.ValidateImageAsync(It.IsAny<IFormFile>())).ReturnsAsync(validationResponse);

            // Act
            var response = await _imageService.SaveImageAsync(mockImageFile.Object, subfolder);
            var concreteResponse = (BaseResponse<string>)response;

            // Assert
            concreteResponse.StatusCode.Should().Be(StatusCode.InvalidData);
            concreteResponse.Description.Should().Contain("Invalid image format. Supported formats: JPEG, PNG, WebP.");
            _mockImageValidator.Verify(v => v.ValidateImageAsync(mockImageFile.Object), Times.Once);
        }

        [Fact]
        public async Task SaveImageAsync_ImageSizeExceedsLimit_ThrowsException()
        {
            // Arrange
            // Set up an oversized test image file
            string currentDirectory = Directory.GetCurrentDirectory();
            string testImagePath = Path.Combine(currentDirectory, "..", "..", "..", "TestPhotos", "3mb.jpg");
            using var fileStream = File.OpenRead(testImagePath);
            var mockImageFile = CreateMockImageFile("3mb.jpg", "image/jpeg", fileStream.Length, fileStream);

            string subfolder = "game";
            var validationResponse = new BaseResponse<string> // Set up the image validator to return an oversized file response
            {
                StatusCode = StatusCode.InvalidData,
                Description = "Image size exceeds the maximum allowed size (2 MB)."
            };
            _mockImageValidator.Setup(v => v.ValidateImageAsync(It.IsAny<IFormFile>())).ReturnsAsync(validationResponse);

            // Act
            var response = await _imageService.SaveImageAsync(mockImageFile.Object, subfolder);
            var concreteResponse = (BaseResponse<string>)response;

            // Assert
            concreteResponse.StatusCode.Should().Be(StatusCode.InvalidData);
            concreteResponse.Description.Should().Contain("Image size exceeds the maximum allowed size (2 MB).");
            _mockImageValidator.Verify(v => v.ValidateImageAsync(mockImageFile.Object), Times.Once);
        }

        [Fact]
        public async Task SaveImageAsync_NullImageFile_ThrowsException()
        {
            // Arrange
            string subfolder = "game";
            var validationResponse = new BaseResponse<string> // Set up the image validator to return an invalid file response for null input
            {
                StatusCode = StatusCode.InvalidData,
                Description = "Invalid image file."
            };
            _mockImageValidator.Setup(v => v.ValidateImageAsync(null)).ReturnsAsync(validationResponse);

            // Act
            var response = await _imageService.SaveImageAsync(null, subfolder);
            var concreteResponse = (BaseResponse<string>)response;

            // Assert
            concreteResponse.StatusCode.Should().Be(StatusCode.InvalidData);
            concreteResponse.Description.Should().Contain("Invalid image file.");
            _mockImageValidator.Verify(v => v.ValidateImageAsync(null), Times.Once);
        }
    }
}
