using eShopApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopApp.Entity.EntityConfiguration.FluentAPI
{
    /// <summary>
    /// ProductCategory modelinin Fluent API ile edilmiw konfiqurasiyalarini saxlayir.
    /// </summary>
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            /* ProductCategory cedvelimizin 2 dene PrimaryKey-i (yeni: 'Composite Primary Key'-i) olacaq: */
            builder.HasKey(pc => new { pc.CategoryID, pc.ProductID });
        }
    }
}