using Microsoft.EntityFrameworkCore;
using Reborn.Models;

namespace Reborn.Services
{
    public interface IProductsService : IService<Product> {}

    public class ProductsService : IProductsService
    {
        private readonly Context _context;

        public ProductsService(Context context)
        {
            _context = context;
        }

        public async Task<Product> Create(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> FindOneById(int id)
        {
            if (_context.Products == null)
            {
                return null;
            }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }

            return product;
        }

        public async Task<List<Product>?> FindAll()
        {
            if (_context.Products == null)
            {
                return null;
            }
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> Remove(int id)
        {
            if (_context.Products == null)
            {
                return null;
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> Update(int id, Product product)
        {
            if (id != product.id)
            {
                return null;
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
            return (_context.Products?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
