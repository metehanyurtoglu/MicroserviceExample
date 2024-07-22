using Discount.gRPC.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Discount.gRPC.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductName = "IPhone 15", Description = "IPhone Discount", Amount = 150 },
                new Coupon { Id = 2, ProductName = "Samsung S23", Description = "Samsung Discount", Amount = 100 }
            );
        }
    }
}
