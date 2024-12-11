using Microsoft.EntityFrameworkCore;
using Reborn.Dto;
using Reborn.Models;

namespace Reborn.Services
{

    public interface IOrderService {
        public Task<Order> Create(Order dto);
        public Task<List<Order>?> FindAll();
        public Task<Order?> FindOneById(int id);
        public Task<Order?> Update(int id, Order model);
        public Task<Order?> Remove(int id);
        public Task<List<Order>> GetAAllByProducts(string name_product);
        public Task<List<Order>> GetAAllByUser( int user_id);
    }
    public class OrdersService : IOrderService
    {
        private readonly Context context;
        public OrdersService(Context contex) {
            this.context = contex;
        }
        async public Task<Order> Create(Order model) {
            context.Orders.Add(model);
            await context.SaveChangesAsync();
            return model; 
        }
        async public Task<List<Order>> FindAll() {
            if (context.Orders == null) return null;
            return await context.Orders.ToListAsync();
        }
        public async Task<Order?> FindOneById(int id)
        {
            if (context.Orders == null)return null;
            var order = await context.Orders.FindAsync(id);
            if (order == null) return null;
            return order;
        }
        public async Task<Order?> Update(int id, Order order)
        {
            if (id != order.id) return null;
      
            context.Entry(order).State = EntityState.Modified;

            try{await context.SaveChangesAsync();}
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id)) return null;
                else throw;
            }
            return order;
        }

        private bool OrderExists(int id)
        {
            return (context.Orders?.Any(e => e.id == id)).GetValueOrDefault();
        }
        async public Task<Order> FindByOrder(Order order) {
            if (context.Orders == null && await context.Orders.FindAsync(order) == null) return null;
            return await context.Orders.FindAsync(order);
        }
        async public Task<Order> Remove(int id) {
            if (context.Orders == null) return null;
            var order = await context.Orders.FindAsync(id);
            if (order == null) return null;
            context.Orders.Remove(order);
            return order;
        }

        public async Task<List<Order>> GetAAllByProducts(string name_product)
        {
            List<Order> listProducts = new List<Order>();
            var orders = await context.Orders?.FirstOrDefaultAsync();
            foreach (var items in listProducts)
            {
                if(items.Product.name.ToString() == name_product) listProducts.Add(items);
            }
            return listProducts;
        }

        public async Task<List<Order>> GetAAllByUser(int user_id)
        {
            List<Order> listUsers = new List<Order>();
            var orders = await context.Orders?.FirstOrDefaultAsync();
            foreach (var user in listUsers)
            {
                if (user.User_Id == user_id) listUsers.Add(user);
            }
            return listUsers;
        }
    }
}
