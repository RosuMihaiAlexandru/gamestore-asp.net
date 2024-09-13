using Gamesmarket.DAL;
using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.Enum;
using Gamesmarket.Domain.Response;
using Gamesmarket.Domain.ViewModel.Identity;
using Gamesmarket.Interfaces.Services;
using Gamesmarket.Service.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Gamesmarket.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<User> userManager, ApplicationDbContext context, ITokenService tokenService, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<IBaseResponse<AuthResponse>> Authenticate(AuthRequest request)
        {
            var baseResponse = new BaseResponse<AuthResponse>();
            try
            {
                var managedUser = await _userManager.FindByEmailAsync(request.Email);

                if (managedUser == null)
                {
                    baseResponse.Description = "Invalid email or password.";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
                if (!isPasswordValid)
                {
                    baseResponse.Description = "Invalid email or password.";
                    baseResponse.StatusCode = StatusCode.InvalidData;
                    return baseResponse;
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (user == null)
                {
                    baseResponse.Description = "Unauthorized access.";
                    baseResponse.StatusCode = StatusCode.Unauthorized;
                    return baseResponse;
                }

                var roleIds = await _context.UserRoles.Where(r => r.UserId == user.Id).Select(x => x.RoleId).ToListAsync();
                var roles = await _context.Roles.Where(x => roleIds.Contains(x.Id)).ToListAsync();

                // Create an access token and refresh token for the user
                var accessToken = _tokenService.CreateToken(user, roles);
                user.RefreshToken = _configuration.GenerateRefreshToken();
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configuration.GetSection("Jwt:RefreshTokenValidityInDays").Get<int>());

                await _context.SaveChangesAsync(); // Save changes to the db

                string userRole = roles.FirstOrDefault()?.Name ?? "No_Role"; // Get the role of the user

                baseResponse.Data = new AuthResponse
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Name = user.Name,
                    Token = accessToken,
                    RefreshToken = user.RefreshToken,
                    Role = userRole
                };
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[Authenticate] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<AuthResponse>> Register(RegisterRequest request)
        {
            var baseResponse = new BaseResponse<AuthResponse>();
            try
            {
                var allowedDomains = new[] { "@gmail.com", "@ukr.net", "@yahoo.com" };
                if (!allowedDomains.Any(domain => request.Email.EndsWith(domain, StringComparison.OrdinalIgnoreCase)))
                {
                    baseResponse.Description = "Invalid email domain. Allowed domains are @gmail.com, @ukr.net, @yahoo.com.";
                    baseResponse.StatusCode = StatusCode.InvalidData;
                    return baseResponse;
                }

                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    UserName = request.Email
                };

                var result = await _userManager.CreateAsync(user, request.Password); // Create the user in the identity model
                if (!result.Succeeded)
                {
                    baseResponse.Description = string.Join(", ", result.Errors.Select(e => e.Description));
                    baseResponse.StatusCode = StatusCode.InvalidData;
                    return baseResponse;
                }

                var findUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
                if (findUser == null)
                {
                    baseResponse.Description = $"User {request.Email} not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                await _userManager.AddToRoleAsync(findUser, RoleConsts.User);  // Give to the new user the 'User' role

                var cart = new Cart { UserId = findUser.Id }; // Create a cart for new user
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();

                baseResponse = await Authenticate(new AuthRequest { Email = request.Email, Password = request.Password }) as BaseResponse<AuthResponse>;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[Register] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<TokenModel>> RefreshToken(TokenModel tokenModel)
        {
            var baseResponse = new BaseResponse<TokenModel>();
            try
            {
                // Get the principal from the expired access token
                var principal = _configuration.GetPrincipalFromExpiredToken(tokenModel.AccessToken);
                if (principal == null)
                {
                    baseResponse.Description = "Invalid access token or refresh token";
                    baseResponse.StatusCode = StatusCode.Unauthorized;
                    return baseResponse;
                }

                // Extract username from the principal and find user
                var username = principal.Identity!.Name;
                var user = await _userManager.FindByNameAsync(username!);

                if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                {
                    baseResponse.Description = "Invalid access token or refresh token";
                    baseResponse.StatusCode = StatusCode.Unauthorized;
                    return baseResponse;
                }

                // Create a new access token and refresh token
                var newAccessToken = _configuration.CreateToken(principal.Claims.ToList());
                var newRefreshToken = _configuration.GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                await _userManager.UpdateAsync(user);

                baseResponse.Data = new TokenModel
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    RefreshToken = newRefreshToken
                };
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[RefreshToken] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> RevokeUserToken(string username)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    baseResponse.Description = "Invalid user name";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    baseResponse.Data = false;
                    return baseResponse;
                }

                user.RefreshToken = null; // Revoke the refresh token by setting it to null
                await _userManager.UpdateAsync(user);
                baseResponse.Data = true;
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[RevokeUserToken] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
                baseResponse.Data = false;
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> RevokeAllTokens()
        {
            var baseResponse = new BaseResponse<bool> { Data = true };
            try
            {
                var users = _userManager.Users.ToList();
                foreach (var user in users) // Iterate through all users and revoke their refresh tokens
                {
                    user.RefreshToken = null;
                    await _userManager.UpdateAsync(user);
                }
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[RevokeAllTokens] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
                baseResponse.Data = false;
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<UserDto>>> GetUsers()
        {
            var baseResponse = new BaseResponse<IEnumerable<UserDto>>();
            try
            {
                var users = await _userManager.Users // Retrieve all users from the database
                    .Select(u => new UserDto
                    {
                        Id = u.Id,
                        Name = u.Name,
                        RefreshTokenExpiryTime = u.RefreshTokenExpiryTime,
                        Email = u.Email
                    })
                    .ToListAsync();

                if (!users.Any())
                {
                    baseResponse.Description = "No users found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.Id.ToString());
                    var roles = await _userManager.GetRolesAsync(appUser);
                    user.Role = roles.FirstOrDefault();
                }

                baseResponse.Data = users;
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[GetUsers] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<string>> ChangeUserRole(ChangeRoleRequest request)
        {
            var baseResponse = new BaseResponse<string>();
            try
            {
                var role = await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == request.NewRole.ToUpper());
                if (role == null)
                {
                    baseResponse.Description = "Requested role does not exist.";
                    baseResponse.StatusCode = StatusCode.InvalidData;
                    return baseResponse;
                }

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                // Prevent changing the role of the admin user
                if (user.Email.Equals("admin@gmail.com", StringComparison.OrdinalIgnoreCase))
                {
                    baseResponse.Description = "Cannot change the role of the admin user.";
                    baseResponse.StatusCode = StatusCode.InvalidData;
                    return baseResponse;
                }

                // Remove the user current role
                var currentRoles = await _userManager.GetRolesAsync(user);
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    baseResponse.Description = "Failed to remove current roles.";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                // Assign the user to the new role
                var addResult = await _userManager.AddToRoleAsync(user, role.Name);
                if (!addResult.Succeeded)
                {
                    baseResponse.Description = "Failed to assign the new role.";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = $"Role of user '{user.Email}' changed to '{request.NewRole}'.";
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[ChangeUserRole] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }
            return baseResponse;
        }
    }
}
