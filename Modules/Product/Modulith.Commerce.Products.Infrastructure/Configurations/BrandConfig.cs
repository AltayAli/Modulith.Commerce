using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Commerce.Products.Domain.Brands;

namespace Modulith.Commerce.Products.Infrastructure.Configurations
{
    public class BrandConfig : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(n => n.Value).HasColumnName("Name").IsRequired();
            });

        }
    }
}
