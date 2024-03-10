using Microsoft.EntityFrameworkCore;
using Reborn.Models;

namespace Reborn.Services
{

    public interface IUsersService : IService<User>
    {
        public Task<User?> FindOneByEmail(string email);
    }

    public class UsersService : IUsersService
    {
        private readonly Context _context;

        public UsersService(Context context)
        {
            _context = context;
        }

        async public Task<User> Create(User model)
        {
               _context.User.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        async public Task<List<User>?> FindAll()
        {
            if (_context.User == null)
            {
                return null;
            }
            return await _context.User.ToListAsync();
        }

        async public Task<User?> FindOneByEmail(string email)
        {
            if (_context.User == null)
            {
                return null;
            }
            var user = await (_context.User?.FirstOrDefaultAsync(e => e.email == email));

            return user;
        }

        async public Task<User?> FindOneById(int id)
        {
            if (_context.User == null)
            {
                return null;
            }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        async public Task<User?> Remove(int id)
        {
            if (_context.User == null)
            {
                return null;
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        async public Task<User?> Update(int id, User model)
        {
            if (id != model.id)
            {
                return null;
            }

            _context.Entry(model).State = EntityState.Modified;

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

            return model;
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
