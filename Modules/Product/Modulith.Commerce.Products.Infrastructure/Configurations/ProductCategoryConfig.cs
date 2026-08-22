using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Commerce.Products.Domain.ProductCategories;

namespace Modulith.Commerce.Products.Infrastructure.Configurations
{
    public class ProductCategoryConfig : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.ProductId, x.CategoryId }).IsUnique();
        }
    }
}
