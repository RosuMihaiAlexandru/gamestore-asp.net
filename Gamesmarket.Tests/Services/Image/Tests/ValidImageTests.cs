using FluentAssertions;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;
using Gamesmarket.Tests.Services.Image.Base;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Gamesmarket.Tests.Services.Image.Tests
{
    public class ValidImageTests : ImageServiceTestBase
    {
        [Fact]
        public async Task SaveImageAsync_ValidImage_Success()
        {
            // Arrange
            // Set up a valid test image file
            string currentDirectory = Directory.GetCurrentDirectory();
            string testImagePath = Path.Combine(currentDirectory, "..", "..", "..", "TestPhotos", "testpic.jpg");
            using var fileStream = File.OpenRead(testImagePath);
            var mockImageFile = CreateMockImageFile("testpic.jpg", "image/jpeg", fileStream.Length, fileStream);

            string subfolder = "game";
            string mockWebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            _mockHostingEnvironment.Setup(h => h.WebRootPath).Returns(mockWebRootPath); // Set up the mock hosting environment to return a specific web root path

            // Set up the image validator to return a successful validation response
            var validationResponse = new BaseResponse<string> { StatusCode = StatusCode.OK };
            _mockImageValidator.Setup(v => v.ValidateImageAsync(It.IsAny<IFormFile>())).ReturnsAsync(validationResponse);

            // Set up the file manager to return a specific saved path
            var savedPath = Path.Combine("images", subfolder, "testpic.jpg").Replace('\\', '/');
            _mockFileManager.Setup(f => f.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>())).ReturnsAsync(savedPath);

            // Act
            var response = await _imageService.SaveImageAsync(mockImageFile.Object, subfolder);
            string savedImagePath = response.Data;

            // Assert
            // Verify the saved image path is as expected
            string expectedPath = Path.Combine("images", subfolder).Replace('\\', '/');
            string actualPath = savedImagePath.Replace('\\', '/');
            string fullSavedPath = Path.Combine(mockWebRootPath, savedImagePath);

            savedImagePath.Should().NotBeNullOrEmpty();
            actualPath.Should().StartWith(expectedPath);
            _mockFileManager.Verify(f => f.SaveFileAsync(mockImageFile.Object, subfolder), Times.Once);
        }
    }
}
