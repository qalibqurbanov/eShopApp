
using eShopApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopApp.Entity.EntityConfiguration.FluentAPI
{
    /// <summary>
    /// Order modelinin Fluent API ile edilmiw konfiqurasiyalarini saxlayir.
    /// </summary>
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(prop => prop.ID);
        }
    }
}