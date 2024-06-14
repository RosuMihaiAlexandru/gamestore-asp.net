using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gamesmarket.Interfaces.Services;
using Gamesmarket.DAL;
using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.ViewModel.Identity;
using Gamesmarket.Service.Extensions;

namespace Gamesmarket.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AccountsController(ITokenService tokenService, ApplicationDbContext context, UserManager<User> userManager, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Find the user by email
            var managedUser = await _userManager.FindByEmailAsync(request.Email);

            if (managedUser == null)
            {
                return BadRequest("Bad credentials");
            }
            // Check if entered password is valid
            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);

            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }
            // Retrieve the user from the db
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (user is null)
                return Unauthorized();

            // Retrieve user roles
            var roleIds = await _context.UserRoles.Where(r => r.UserId == user.Id).Select(x => x.RoleId).ToListAsync();
            var roles = _context.Roles.Where(x => roleIds.Contains(x.Id)).ToList();

            // Create an access token and refresh token for the user
            var accessToken = _tokenService.CreateToken(user, roles);
            user.RefreshToken = _configuration.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configuration.GetSection("Jwt:RefreshTokenValidityInDays").Get<int>());

            // Save changes to the db
            await _context.SaveChangesAsync();

            // Get the role of the user for frontend
            string userRole = roles.FirstOrDefault()?.Name ?? "No_Role";

            // Return the authentication response
            return Ok(new AuthResponse
            {
                Username = user.UserName!,
                Email = user.Email!,
                Name = user.Name!,
                Token = accessToken,
                RefreshToken = user.RefreshToken,
                Role = userRole
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(request);

            // Ensure the email domain is allowed
            var allowedDomains = new[] { "@gmail.com", "@ukr.net", "@yahoo.com" };
            if (!allowedDomains.Any(domain => request.Email.EndsWith(domain, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest("Invalid email domain. Allowed domains are @gmail.com, @ukr.net, @yahoo.com.");
            }

            // Create a new user based on the registration request
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                UserName = request.Email
            };
            // Create the user in the identity model
            var result = await _userManager.CreateAsync(user, request.Password);

            foreach (var error in result.Errors)// Add errors to ModelState if the user creation fails
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            if (!result.Succeeded) return BadRequest(request);

            // Find the newly created user
            var findUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (findUser == null) throw new Exception($"User {request.Email} not found");

            // Give to the new user the 'User' role
            await _userManager.AddToRoleAsync(findUser, RoleConsts.User);

            // Create a cart for new user
            var cart = new Cart
            {
                UserId = findUser.Id,
            };

            // Save the cart to db
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            // Authenticate the user and return the authentication response
            return await Authenticate(new AuthRequest
            {
                Email = request.Email,
                Password = request.Password
            });
        }

        [Authorize]
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            // Extract access and refresh tokens from the request
            var accessToken = tokenModel.AccessToken;
            var refreshToken = tokenModel.RefreshToken;

            // Get the principal from the expired access token
            var principal = _configuration.GetPrincipalFromExpiredToken(accessToken);

            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            // Extract username from the principal and find user
            var username = principal.Identity!.Name;
            var user = await _userManager.FindByNameAsync(username!);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            // Create a new access token and refresh token
            var newAccessToken = _configuration.CreateToken(principal.Claims.ToList());
            var newRefreshToken = _configuration.GenerateRefreshToken();

            // Update the user's refresh token and save changes
            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        [Authorize("AdminPolicy")]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);// Find the user by username
            if (user == null) return BadRequest("Invalid user name");

            user.RefreshToken = null;// Revoke the refresh token by setting it to null
            await _userManager.UpdateAsync(user);// Update the user

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();// Get the list of all users
            foreach (var user in users)// Iterate through all users and revoke their refresh tokens
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpGet("getUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            // Retrieve all users from the database
            var users = await _userManager.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    RefreshTokenExpiryTime = u.RefreshTokenExpiryTime,
                    Email = u.Email
                })
                .ToListAsync();

            if (users == null || users.Count == 0)
            {
                return NotFound("No users found");
            }

            foreach (var user in users)
            {
                var appUser = await _userManager.FindByIdAsync(user.Id.ToString());
                var roles = await _userManager.GetRolesAsync(appUser);
                user.Role = roles.FirstOrDefault();
            }

            return Ok(users);
        }

        [Authorize("AdminPolicy")]
        [HttpPost]
        [Route("change-role")]
        public async Task<IActionResult> ChangeUserRole(ChangeRoleRequest request)
        {
            // Ensure the requested role exists
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == request.NewRole.ToUpper());
            if (role == null) return BadRequest("Requested role does not exist.");

            // Find the user by email
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return BadRequest("User not found");

            // Prevent changing the role of the admin user
            if (user.Email.Equals("admin@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Cannot change the role of the admin user.");
            }

            // Remove the user current role
            var currentRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!result.Succeeded)
            {
                return BadRequest("Failed to remove current roles.");
            }

            // Assign the user to the new role
            result = await _userManager.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded)
            {
                return BadRequest("Failed to assign the new role.");
            }

            return Ok($"Role of user '{user.Email}' changed to '{request.NewRole}'.");
        }

    }
}
