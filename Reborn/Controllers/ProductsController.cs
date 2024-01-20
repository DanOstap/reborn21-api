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
            return await _productsService.GetProducts();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
          return await _productsService.GetOneProductById(id);
        }

        [HttpPut("{id}")]
        public async Task<Product> PutProduct(int id, Product product)
        {
            return await _productsService.Update(id, product);
        }

        [HttpDelete("{id}")]
        public async Task<Product> DeleteProduct(int id)
        {
            return await _productsService.Remove(id);
        }
    }
}
