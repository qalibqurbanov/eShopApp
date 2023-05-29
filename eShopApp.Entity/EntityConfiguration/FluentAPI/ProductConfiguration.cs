using eShopApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopApp.Entity.EntityConfiguration.FluentAPI
{
    /// <summary>
    /// Product modelinin Fluent API ile edilmiw konfiqurasiyalarini saxlayir.
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(prop => prop.ProductID);
        }
    }
}