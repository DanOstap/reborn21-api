using Microsoft.EntityFrameworkCore;
using Reborn.Dto;
using Reborn.Models;

namespace Reborn.Services
{

    public interface IUsersService
    {
        public Task<User> Create(CreateUserDto dto);
        public Task<User?> FindOneByEmail(string email);
        public Task<User?> FindOneByActivationLink(string activationLink);
        public Task<List<User>?> FindAll();
        public Task<User?> FindOneById(int id);
        public Task<User?> Update(int id, User model);
        public Task<User?> Remove(int id);
    }

    public class UsersService : IUsersService
    {
        private readonly Context context;

        public UsersService(Context context)
        {
            this.context = context;
        }

        async public Task<User> Create(CreateUserDto dto)
        {
            User user = new User();

            user.email = dto.email;
            user.password = dto.password;
            user.activationLink = this.GenerateRandomString(10);

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
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
                if (!UserExists(id))
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

        private bool UserExists(int id)
        {
            return (context.Users?.Any(e => e.id == id)).GetValueOrDefault();
        }

        private string GenerateRandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var stringChars = new char[length];
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new String(stringChars);
        }
    }
}
