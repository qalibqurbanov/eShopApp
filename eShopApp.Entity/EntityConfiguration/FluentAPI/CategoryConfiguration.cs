using eShopApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopApp.Entity.EntityConfiguration.FluentAPI
{
    /// <summary>
    /// Category modelinin Fluent API ile edilmiw konfiqurasiyalarini saxlayir.
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(prop => prop.CategoryID);
        }
    }
}