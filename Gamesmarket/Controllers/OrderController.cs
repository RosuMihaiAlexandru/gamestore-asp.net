using Gamesmarket.Domain.ViewModel.Order;
using Gamesmarket.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamesmarket.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel model)
        {
             var response = await _orderService.Create(model);
             if (response.StatusCode == Domain.Enum.StatusCode.OK)
             {
                 return Ok(new { data = response.Data, description = "Order added successfully." });
             }
             return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _orderService.Delete(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(new { description = "Order deleted successfully." });
            }
            return BadRequest("Failed to delete order");
        }
    }
}
