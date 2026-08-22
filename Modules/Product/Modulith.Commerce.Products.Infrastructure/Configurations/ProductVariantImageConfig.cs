using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Commerce.Products.Domain.ProductVariantImages;

namespace Modulith.Commerce.Products.Infrastructure.Configurations
{
    public class ProductVariantImageConfig : IEntityTypeConfiguration<ProductVariantImage>
    {
        public void Configure(EntityTypeBuilder<ProductVariantImage> builder)
        {
            builder.ToTable("ProductVariantImages");
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.ImageUrl, name =>
            {
                name.Property(n => n.Url).HasColumnName("ImageUrl").IsRequired();
            });

            builder.OwnsOne(x => x.ImageUrl, name =>
            {
                name.Property(n => n.Url).HasColumnName("ImageUrl").IsRequired();
            });
        }
    }
}
