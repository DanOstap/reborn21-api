using Microsoft.EntityFrameworkCore;
using Reborn.Models;

namespace Reborn
{
    public class Context : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>().HasKey("id");
            builder.Entity<Order>().HasKey("id");
            builder.Entity<User>().HasKey("id");

        }
    }
}


