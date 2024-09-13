using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gamesmarket.Interfaces.Services;
using Gamesmarket.Domain.ViewModel.Identity;
using Gamesmarket.Domain.Response;

namespace Gamesmarket.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponse<AuthResponse>>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _accountService.Authenticate(request);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return Ok(response);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<BaseResponse<AuthResponse>>> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(request);

            var response = await _accountService.Register(request);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return Ok(response);

            return StatusCode((int)response.StatusCode, response);
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
        {
            if (tokenModel is null) return BadRequest("Invalid client request");

            var response = await _accountService.RefreshToken(tokenModel);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return Ok(response);

            return StatusCode((int)response.StatusCode, response);
        }

        [Authorize("AdminPolicy")]
        [HttpPost("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var response = await _accountService.RevokeUserToken(username);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return Ok(response);

            return StatusCode((int)response.StatusCode, response);
        }

        [Authorize("AdminPolicy")]
        [HttpPost("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var response = await _accountService.RevokeAllTokens();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return Ok(response);

            return StatusCode((int)response.StatusCode, response);
        }

        [Authorize("AdminPolicy")]
        [HttpGet("getUsers")]
        public async Task<ActionResult<BaseResponse<IEnumerable<UserDto>>>> GetUsers()
        {
            var response = await _accountService.GetUsers();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return Ok(response.Data.ToList());

            return StatusCode((int)response.StatusCode, response);
        }

        [Authorize("AdminPolicy")]
        [HttpPost("change-role")]
        public async Task<IActionResult> ChangeUserRole(ChangeRoleRequest request)
        {
            var response = await _accountService.ChangeUserRole(request);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return Ok(response);

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
