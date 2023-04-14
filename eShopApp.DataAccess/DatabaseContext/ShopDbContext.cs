using eShopApp.Entity.Entities;
using eShopApp.Entity.EntityConfiguration.FluentAPI;
using Microsoft.EntityFrameworkCore;

namespace eShopApp.DataAccess.DatabaseContext
{
    /// <summary>
    /// DB ile data mubadilesi etmek ucun iwledeceyimiz DbContext sinifi.
    /// </summary>
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> option) : base(option) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            //modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
        }
    }
}