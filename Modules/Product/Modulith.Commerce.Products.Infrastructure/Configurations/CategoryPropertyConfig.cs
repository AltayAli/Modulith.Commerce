using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Commerce.Products.Domain.CategoryProperties;

namespace Modulith.Commerce.Products.Infrastructure.Configurations
{
    public class CategoryPropertyConfig : IEntityTypeConfiguration<CategoryProperty>
    {
        public void Configure(EntityTypeBuilder<CategoryProperty> builder)
        {
            builder.ToTable("CategoryProperties");
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(n => n.Value).HasColumnName("Name").IsRequired();
            });
        }
    }
}
