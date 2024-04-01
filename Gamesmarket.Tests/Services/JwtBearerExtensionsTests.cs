using Xunit;
using Moq;
using FluentAssertions;
using Gamesmarket.Service.Extensions;
using Gamesmarket.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Gamesmarket.Tests.Services
{
    public class JwtBearerExtensionsTests
    {
        private readonly Mock<IConfiguration> _configuration;

        public JwtBearerExtensionsTests()
        {
            _configuration = new Mock<IConfiguration>();
        }

        [Fact]
        public void CreateClaims_ValidUserAndRoles_ReturnsExpectedClaims()
        {
            // Arrange
            var user = new User { Id = 1, UserName = "testuser", Email = "test@example.com" };
            var roles = new List<IdentityRole<long>> { new IdentityRole<long> { Name = "User" }};

            // Act
            var claims = user.CreateClaims(roles);

            // Assert
            claims.Should().NotBeNullOrEmpty();
            claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.Jti);
            claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.Iat);
            claims.Should().Contain(c => c.Type == ClaimTypes.NameIdentifier && c.Value == user.Id.ToString());
            claims.Should().Contain(c => c.Type == ClaimTypes.Name && c.Value == user.UserName);
            claims.Should().Contain(c => c.Type == ClaimTypes.Email && c.Value == user.Email);
            claims.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == "User");
        }

        [Fact]
        public void CreateSigningCredentials_ValidConfiguration_ReturnsSigningCredentials()
        {
            // Arrange
            _configuration.Setup(c => c["Jwt:Secret"]).Returns("testsecret");

            // Act
            var signingCredentials = _configuration.Object.CreateSigningCredentials();

            // Assert
            signingCredentials.Should().NotBeNull();
            signingCredentials.Key.Should().BeOfType<SymmetricSecurityKey>();
            signingCredentials.Algorithm.Should().Be(SecurityAlgorithms.HmacSha256);
        }

        [Fact]
        public void CreateJwtToken_ValidClaimsAndConfiguration_ReturnsJwtSecurityToken()
        {
            // Arrange
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, "testuser") };
            _configuration.Setup(c => c["Jwt:Secret"]).Returns("testsecret");
            _configuration.Setup(c => c["Jwt:Issuer"]).Returns("testissuer");
            _configuration.Setup(c => c["Jwt:Audience"]).Returns("testaudience");
            _configuration.SetupGet(c => c.GetSection("Jwt:Expire").Value).Returns("30");

            // Act
            var jwtToken = claims.CreateJwtToken(_configuration.Object);

            // Assert
            jwtToken.Should().NotBeNull();
            jwtToken.Issuer.Should().Be("testissuer");
            jwtToken.Audiences.Should().Contain("testaudience");
            jwtToken.ValidTo.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(30), TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void CreateToken_ValidClaimsAndConfiguration_ReturnsJwtSecurityToken()
        {
            // Arrange
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, "testuser") };
            _configuration.Setup(c => c["Jwt:Secret"]).Returns("testsecret");
            _configuration.Setup(c => c["Jwt:Issuer"]).Returns("testissuer");
            _configuration.Setup(c => c["Jwt:Audience"]).Returns("testaudience");
            _configuration.SetupGet(c => c.GetSection("Jwt:TokenValidityInMinutes").Value).Returns("30");

            // Act
            var jwtToken = _configuration.Object.CreateToken(claims);

            // Assert
            jwtToken.Should().NotBeNull();
            jwtToken.Issuer.Should().Be("testissuer");
            jwtToken.Audiences.Should().Contain("testaudience");
            jwtToken.ValidTo.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(30), TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void GenerateRefreshToken_ReturnsNonEmptyString()
        {
            // Arrange

            // Act
            var refreshToken = _configuration.Object.GenerateRefreshToken();

            // Assert
            refreshToken.Should().NotBeNullOrEmpty();
            refreshToken.Should().HaveLength(88);
        }

    }
}
