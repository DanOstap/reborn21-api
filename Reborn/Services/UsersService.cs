using Microsoft.EntityFrameworkCore;
using Reborn.Models;

namespace Reborn.Services
{

    public interface IUsersService : IService<User>
    {
        public Task<User?> FindOneByEmail(string email);
        public Task<User?> FindOneByActivationLink(string activationLink);
    }

    public class UsersService : IUsersService
    {
        private readonly Context context;

        public UsersService(Context context)
        {
            this.context = context;
        }

        async public Task<User> Create(User model)
        {
               context.Users.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        async public Task<List<User>?> FindAll()
        {
            if (context.Users == null)
            {
                return null;
            }
            return await context.Users.ToListAsync();
        }

        async public Task<User?> FindOneByEmail(string email)
        {
            if (context.Users == null) return null;

            var user = await (context.Users?.FirstOrDefaultAsync(e => e.email == email));

            return user;
        }

        async public Task<User?> FindOneByActivationLink(string activationLink)
        {
            if (context.Users == null) return null;

            var user = await (context.Users?.FirstOrDefaultAsync(e => e.activationLink == activationLink));

            return user;
        }

        async public Task<User?> FindOneById(int id)
        {
            if (context.Users == null) 
            {
                return null;
            }
            var user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        async public Task<User?> Remove(int id)
        {
            if (context.Users == null)
            {
                return null;
            }
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return user;
        }

        async public Task<User?> Update(int id, User model)
        {
            if (id != model.id)
            {
                return null;
            }

            context.Entry(model).State = EntityState.Modified;

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

            return model;
        }

        private bool ProductExists(int id)
        {
            return (context.Products?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
