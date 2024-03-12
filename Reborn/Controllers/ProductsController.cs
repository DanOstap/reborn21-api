using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reborn.Models;
using Reborn.Services;

namespace Reborn.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
        {
         
            return Ok(await productsService.Create(product));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Products = await productsService.FindAll();
            return Ok(Products);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var Product = await productsService.FindOneById(id);
            if (Product == null) return NotFound(new {message = "Product not found."});
            return Ok(Product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(int id, Product product)
        {
            var Updated_Product = await productsService.Update(id, product);
            if (Updated_Product == null) return NotFound(new { message = "Product not found." });
            return Ok(Updated_Product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var Deleted_Product = await productsService.Remove(id);
            return Ok(Deleted_Product);
        }
    }
}
