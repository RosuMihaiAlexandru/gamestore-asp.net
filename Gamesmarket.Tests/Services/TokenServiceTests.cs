using Xunit;
using Moq;
using FluentAssertions;
using Gamesmarket.Domain.Entity;
using Gamesmarket.Service.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Gamesmarket.Tests.Services
{
    public class TokenServiceTests
    {
        private readonly Mock<IConfiguration> _configuration;

        public TokenServiceTests()
        {
            _configuration = new Mock<IConfiguration>();
        }

        [Fact]
        public void CreateToken_ValidUserAndRoles_ReturnsJwtTokenString()
        {
            // Arrange
            var user = new User { Id = 1, UserName = "testuser", Email = "test@example.com" };
            var roles = new List<IdentityRole<long>> { new IdentityRole<long> { Name = "User" } };
            _configuration.Setup(c => c["Jwt:Secret"]).Returns("12345678910lovaevedcfadaweascret_key");
            _configuration.Setup(c => c["Jwt:Issuer"]).Returns("testissuer");
            _configuration.Setup(c => c["Jwt:Audience"]).Returns("testaudience");
            _configuration.SetupGet(c => c.GetSection("Jwt:Expire").Value).Returns("30");

            var tokenService = new TokenService(_configuration.Object);

            // Act
            var result = tokenService.CreateToken(user, roles);

            // Assert
            result.Should().NotBeNullOrEmpty();
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(result); // Parse the token string
            jwtToken.Claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.Jti);
            jwtToken.Claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.Iat);
            jwtToken.Claims.Should().Contain(c => c.Type == ClaimTypes.NameIdentifier && c.Value == user.Id.ToString());
            jwtToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Name && c.Value == user.UserName);
            jwtToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Email && c.Value == user.Email);
            jwtToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == "User");
            jwtToken.Issuer.Should().Be("testissuer");
            jwtToken.Audiences.Should().Contain("testaudience");
            jwtToken.ValidTo.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(30), TimeSpan.FromSeconds(1));
        }
    }
}
