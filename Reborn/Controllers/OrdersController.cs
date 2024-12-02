using Microsoft.AspNetCore.Mvc;
using Reborn.Models;
using Reborn.Services;

namespace Reborn.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService service;

        public OrdersController(IOrderService service)
        {
            this.service = service;
        }
     
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await service.FindAll());
        }
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody] Order order)
        {
            var product = await service.Create(order);
            return Ok(product);
        }
        [HttpPatch]
        public async Task<ActionResult<Order>> ChangeStatus(int id, Order order)
        {
            var Order = await service.Update(id, order);
            return (Order == null) ? Ok() : NotFound(new { message = $"Order by if {id} not found." });
        }

        [HttpGet]
        public async Task<List<Order>> GetOrdersByProduct( string product)
        {
            
            return await service.GetAAllByProducts(product);
        }
    }
}
