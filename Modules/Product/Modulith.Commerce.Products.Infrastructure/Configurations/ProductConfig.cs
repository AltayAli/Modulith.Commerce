using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Commerce.Products.Domain.Products;

namespace Modulith.Commerce.Products.Infrastructure.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description);
            builder.Property(x => x.ShortDescription).HasMaxLength(500);

            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(n => n.Value).HasColumnName("Name").IsRequired();
            });

            builder.OwnsOne(x => x.Slug, slug =>
            {
                slug.Property(s => s.Value).HasColumnName("Slug").HasMaxLength(280).IsRequired();
                slug.HasIndex(s => s.Value).IsUnique().HasFilter("[DeletedDate] IS NULL");
            });

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.TaxClass)
                .HasMaxLength(50)
                .HasDefaultValue("standard")
                .IsRequired();

            builder.OwnsOne(x => x.Seo, seo => seo.ToJson());

            builder.Property(x => x.AvgRating)
                .HasPrecision(3, 2)
                .HasDefaultValue(0);

            builder.Property(x => x.ReviewCount)
                .HasDefaultValue(0);

            builder.Property(x => x.PublishedAt);
        }
    }
}
