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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>().HasKey(p => p.product_id);
            builder.Entity<Order>().HasKey(o => o.order_id);
            builder.Entity<User>().HasKey(u => u.user_id);

            builder.Entity<Order>()
                   .HasOne(o => o.User)
                   .WithMany(u => u.Order)
                   .HasForeignKey(o => o.user_id);

            builder.Entity<Order>()
                   .HasOne(o => o.Product)
                   .WithMany(p => p.Order)
                   .HasForeignKey(o => o.product_Id);
        }
    }
}
