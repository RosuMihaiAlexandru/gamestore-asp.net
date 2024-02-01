using Gamesmarket.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamesmarket.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        [HttpGet("getDetail")]
        public async Task<IActionResult> Detail()
        {
            var response = await _cartService.GetItems(User.Identity.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(response.Data.ToList());
            }
            return BadRequest("Operation was not successful");
        }

        [Authorize]
        [HttpGet("getItem")]
        public async Task<IActionResult> GetItem(long id)
        {
            var response = await _cartService.GetItem(User.Identity.Name, id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(response.Data);
            }
            return BadRequest("Operation was not successful");
        }
    }
}
