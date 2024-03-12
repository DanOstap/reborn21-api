using Microsoft.EntityFrameworkCore;
using Reborn.Models;

namespace Reborn.Services
{
    public interface IProductsService : IService<Product> {}

    public class ProductsService : IProductsService
    {
        private readonly Context context;

        public ProductsService(Context context)
        {
            this.context = context;
        }

        public async Task<Product> Create(Product product)
        {
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
