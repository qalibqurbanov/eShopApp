﻿using eShopApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using eShopApp.Entity.EntityConfiguration.FluentAPI;
using System.Reflection;

namespace eShopApp.DataAccess.DatabaseContext
{
    /// <summary>
    /// DB ile data mubadilesi etmek ucun iwledeceyimiz DbContext sinifi.
    /// </summary>
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        public DbSet<Product>         Products        { get; set; }
        public DbSet<Category>        Categories      { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Cart>            Carts           { get; set; }
        public DbSet<CartItem>        CartItems       { get; set; }
        public DbSet<Order>           Orders          { get; set; }
        public DbSet<OrderItem>       OrderItems      { get; set; }

        public override int SaveChanges()
        {
            try
            {
                IEnumerable<EntityEntry<Product>> Result = ChangeTracker.Entries<Product>(); /* Ilk once mudaxile edeceyim entity-ni elde edirem */

                foreach (var data in Result)
                {
                    switch (data.State)
                    {
                        /* Eger hazirki 'Product' entitysi yaradilibsa ve ya uzerinde deyiwiklik edilibse: */
                        case EntityState.Added:
                        case EntityState.Modified:
                        {
                            /* Mehsul yaradilanda ve ya uzerinde deyiwiklik edilende mehsul wekli gosterilmeyibse hemin mehsula default wekil verirem: */
                            if (data.Entity.ProductImageName == null || string.IsNullOrEmpty(data.Entity.ProductImageName))
                            {
                                data.Entity.ProductImageName = "DEFAULT.png";
                            }
                        } break;
                    }
                }

                return base.SaveChanges(); /* Son olaraq ise base classdaki esl 'SaveChanges()' iwlesin */
            }
            catch
            {
                return 0;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            // Ve yaxud qisaca: \\
            // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); /* Hazirki Assembly icerisinde 'IEntityTypeConfiguration' interfeysini implement eden sinifleri vermiw olduq. */
        }
    }
}