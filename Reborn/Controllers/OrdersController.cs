﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Reborn.Models;
using Reborn.Services;

namespace Reborn.Controllers
{
    [Route("api/Orders")]
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
        [HttpGet("/byId")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await service.FindOneById(id));
        }
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody] Order order)
        {
            var product = await service.Create(order);
            return CreatedAtAction("GetOrder", new { id = order.order_id }, order);
        }
        [HttpPut]
        public async Task<ActionResult<Order>> ChangeStatus(int id, Order order)
        {
            var Order = await service.Update(id, order);
            return (Order == null) ? Ok() : NotFound(new { message = $"Order by if {id} not found." });
        }
        [HttpPost("MailTest")]
        public IActionResult Mail( string mail) {
            Console.WriteLine(mail);
            return Ok(mail);
        }
  
    }
}
