using Xunit;
using FluentAssertions;
using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.ViewModel.Identity;
using Gamesmarket.IntegrationTests.Helper;
using GamesMarket.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Gamesmarket.IntegrationTests.Controllers
{
    public class AccountControllerTests
    {
        private readonly WebApplicationFactory<Program> _factory;

        public AccountControllerTests()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
                        services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("test");
                        });
                    });
                });
        }

        [Fact]
        public async Task Authenticate_ValidCredentials_ReturnsAuthResponse()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            // Create a test user with a known password
            var user = new User
            {
                UserName = "testuser",
                Email = "test@gmail.com",
                Name = "Test User"
            };
            var result = await userManager.CreateAsync(user, "TestPassword123!");
            result.Succeeded.Should().BeTrue();

            var request = new AuthRequest
            {
                Email = "test@gmail.com",
                Password = "TestPassword123!"
            };

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync(HttpHelper.Urls.Authenticate, HttpHelper.GetJsonHttpContent(request));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var authResponse = JsonConvert.DeserializeObject<AuthResponse>(await response.Content.ReadAsStringAsync());
            authResponse.Should().NotBeNull();
            authResponse.Username.Should().Be("testuser");
            authResponse.Email.Should().Be("test@gmail.com");
            authResponse.Name.Should().Be("Test User");
            authResponse.Token.Should().NotBeNullOrEmpty();
            authResponse.RefreshToken.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Register_ValidRegistrationRequest_ReturnsAuthResponse()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var cntx = scopedServices.GetRequiredService<ApplicationDbContext>();

                cntx.Database.EnsureDeleted();
                cntx.Database.EnsureCreated();
            }

            var request = new RegisterRequest
            {
                Email = "newuser@gmail.com",
                BirthDate = new DateTime(2000, 01, 01),
                Password = "TestPassword123!",
                PasswordConfirm = "TestPassword123!",
                Name = "New User",
            };

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync(HttpHelper.Urls.Register, HttpHelper.GetJsonHttpContent(request));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var authResponse = JsonConvert.DeserializeObject<AuthResponse>(await response.Content.ReadAsStringAsync());
            authResponse.Should().NotBeNull();
            authResponse.Username.Should().Be("newuser@gmail.com");
            authResponse.Email.Should().Be("newuser@gmail.com");
            authResponse.Name.Should().Be("New User");
            authResponse.Token.Should().NotBeNullOrEmpty();
            authResponse.RefreshToken.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task RefreshToken_ValidCredentials_ReturnsNewTokens()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var cntx = scopedServices.GetRequiredService<ApplicationDbContext>();

                cntx.Database.EnsureDeleted();
                cntx.Database.EnsureCreated();
            }
            var accessHelper = new AccessHelper(_factory);
            var (adminAccessToken, adminRefreshToken) = await accessHelper.GetAdminTokens(); // Get admin tokens

            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminAccessToken);

            var tokenRequest = new TokenModel
            {
                AccessToken = adminAccessToken,
                RefreshToken = adminRefreshToken
            };

            // Act
            var tokenResponse = await client.PostAsync(HttpHelper.Urls.RefreshToken, HttpHelper.GetJsonHttpContent(tokenRequest));

            // Assert
            tokenResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var newTokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(await tokenResponse.Content.ReadAsStringAsync());
            newTokens["accessToken"].Should().NotBeNullOrEmpty();
            newTokens["refreshToken"].Should().NotBeNullOrEmpty();
            newTokens["accessToken"].Should().NotBe(adminAccessToken);
            newTokens["refreshToken"].Should().NotBe(adminRefreshToken);
        }

        [Fact]
        public async Task Revoke_ValidUsernameAndAdminToken_ReturnsOk()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var cntx = scopedServices.GetRequiredService<ApplicationDbContext>();

                cntx.Database.EnsureDeleted();
                cntx.Database.EnsureCreated();
            }
            var usernameToRevoke = "admin@gmail.com";

            var accessHelper = new AccessHelper(_factory);
            var client = await accessHelper.GetAuthorizedClient("admin@gmail.com", "Qwe!23"); // Get authorized client

            // Act
            var response = await client.PostAsync(HttpHelper.Urls.Revoke + usernameToRevoke, null);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            // Verify refresh token is null
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var revokedUser = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == usernameToRevoke);
                revokedUser.Should().NotBeNull();
                revokedUser.RefreshToken.Should().BeNull();
            }
        }

        [Fact]
        public async Task RevokeAll_WithAdminToken_ReturnsOk()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var cntx = scopedServices.GetRequiredService<ApplicationDbContext>();

                cntx.Database.EnsureDeleted();
                cntx.Database.EnsureCreated();
            }
            var accessHelper = new AccessHelper(_factory);
            var client = await accessHelper.GetAuthorizedClient("admin@gmail.com", "Qwe!23"); // Get authorized client

            // Act
            var response = await client.PostAsync(HttpHelper.Urls.RevokeAll, null);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            // Verify all refresh tokens of all users are null
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var usersWithRefreshToken = await dbContext.Users.AnyAsync(u => u.RefreshToken != null);
                usersWithRefreshToken.Should().BeFalse(); // Ensure no users have refresh tokens
            }
        }

        [Fact]
        public async Task GetUsers_WithAdminToken_ReturnsListOfUsers()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var cntx = scopedServices.GetRequiredService<ApplicationDbContext>();

                cntx.Database.EnsureDeleted();
                cntx.Database.EnsureCreated();
            }
            var accessHelper = new AccessHelper(_factory);
            var client = await accessHelper.GetAuthorizedClient("admin@gmail.com", "Qwe!23"); // Get authorized client

            // Act
            var response = await client.GetAsync(HttpHelper.Urls.GetUsers);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var users = JsonConvert.DeserializeObject<List<UserDto>>(await response.Content.ReadAsStringAsync());
            users.Should().NotBeNullOrEmpty();
            users.Should().Contain(u => u.Email == "admin@gmail.com");
        }

        [Fact]
        public async Task ChangeUserRole_WithAdminToken_ReturnsChangedRole()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var cntx = scopedServices.GetRequiredService<ApplicationDbContext>();
                cntx.Database.EnsureDeleted();
                cntx.Database.EnsureCreated();
            }
            // Create a test user with a known password
            var userManager = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<UserManager<User>>();
            var user = new User
            {
                UserName = "testuser",
                Email = "test@gmail.com",
                Name = "Test User"
            };
            var changeRoleRequest = new ChangeRoleRequest
            {
                Email = "test@gmail.com",
                NewRole = "Moderator"
            };

            var accessHelper = new AccessHelper(_factory);
            var client = await accessHelper.GetAuthorizedClient("admin@gmail.com", "Qwe!23"); // Get authorized client

            // Act  
            var addUser = await userManager.CreateAsync(user, "TestPassword123!");
            var response = await client.PostAsync(HttpHelper.Urls.ChangeUserRole, HttpHelper.GetJsonHttpContent(changeRoleRequest));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Contain("Role of user 'test@gmail.com' changed to 'Moderator'"); // Check for success message
            // Ensure the role was updated
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var updatedUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == "test@gmail.com");
                var roles = await userManager.GetRolesAsync(updatedUser);
                roles.Should().Contain("Moderator");
            }
        }
    }
}
