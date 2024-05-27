using Microsoft.EntityFrameworkCore;
using Reborn.Dto;
using Reborn.Models;

namespace Reborn.Services
{
    public interface IProductsService {
        public Task<Product> Create(CreateProductDto dto, IFormFile file);
        public Task<List<Product>?> FindAll();
        public Task<Product?> FindOneById(int id);
        public Task<Product?> Update(int id, Product model);
        public Task<Product?> Remove(int id);
    }

    public class ProductsService : IProductsService
    {
        private readonly Context context;
        private readonly IFileService fileService;

        public ProductsService(Context context, IFileService fileService)
        {
            this.context = context;
            this.fileService = fileService;
        }

        public async Task<Product> Create(CreateProductDto dto, IFormFile file)
        {
            Product product = new Product();

            product.name = dto.name;
            product.description = dto.description;
            product.price = dto.price;
            product.color = dto.color;
            product.size = dto.size;
            product.image = fileService.Upload(file);

            context.Products.Add(product);
            await context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> FindOneById(int id)
        {
            if (context.Products == null)
            {
                return null;
            }
            var product = await context.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }

            return product;
        }

        public async Task<List<Product>?> FindAll()
        {
            if (context.Products == null)
            {
                return null;
            }
            return await context.Products.ToListAsync();
        }

        public async Task<Product?> Remove(int id)
        {
            if (context.Products == null)
            {
                return null;
            }
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> Update(int id, Product product)
        {
            if (id != product.id)
            {
                return null;
            }

            context.Entry(product).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return product;
        }

        private bool ProductExists(int id)
        {
            return (context.Products?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
