using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gamesmarket.Service.Interfaces;
using GamesMarket.DAL;
using Gamesmarket.Domain.Entity;
using Gamesmarket.Domain.ViewModel.Identity;
using Gamesmarket.Service.Extensions;

namespace Gamesmarket.Controllers
{
    [ApiController]
    [Route("api/accounts")]
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

            // Return the authentication response
            return Ok(new AuthResponse
            {
                Username = user.UserName!,
                Email = user.Email!,
                Token = accessToken,
                RefreshToken = user.RefreshToken
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(request);

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

        [Authorize]
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

        [Authorize]
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
    }
}
