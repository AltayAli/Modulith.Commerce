using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Commerce.Products.Domain.CategoryPropertyValues;

namespace Modulith.Commerce.Products.Infrastructure.Configurations
{
    public class CategoryPropertyValueConfig : IEntityTypeConfiguration<CategoryPropertyValue>
    {
        public void Configure(EntityTypeBuilder<CategoryPropertyValue> builder)
        {
            builder.ToTable("CategoryPropertyValues");
            builder.HasKey(x => x.Id);

        }
    }
}
