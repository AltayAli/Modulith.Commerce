using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Commerce.Products.Domain.ProductVariantProperties;

namespace Modulith.Commerce.Products.Infrastructure.Configurations
{
    public class ProductVariantPropertyConfig : IEntityTypeConfiguration<ProductVariantProperty>
    {
        public void Configure(EntityTypeBuilder<ProductVariantProperty> builder)
        {
            builder.ToTable("ProductVariantProperties");
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.ProductVariantId, x.CategoryPropertyId }).IsUnique();
            builder.Property(x => x.Value).HasMaxLength(50);
        }
    }
}
