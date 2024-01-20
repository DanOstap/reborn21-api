using Microsoft.AspNetCore.Mvc;
using Reborn.Models;
using Reborn.Services;

namespace Reborn.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
        {
         
            return await _productsService.Create(product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Products = await _productsService.GetProducts();
            if (Products == null) return NotFound();
            return Ok(Products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var Product = await _productsService.GetOneProductById(id);
            if (Product == null) return NotFound();
            return Ok(Product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(int id, Product product)
        {
            var Updated_Product = await _productsService.Update(id, product);
            if (Updated_Product == null) return BadRequest();
            return Ok(Updated_Product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var Deleted_Product = await _productsService.Remove(id);
            if (Deleted_Product == null) return BadRequest();
            return Ok(Deleted_Product);
           
        }
    }
}
