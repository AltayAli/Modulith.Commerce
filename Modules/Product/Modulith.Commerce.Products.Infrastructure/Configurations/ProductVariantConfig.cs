using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Commerce.Common.Domain.ValueObjects;
using Modulith.Commerce.Products.Domain.ProductVariants;

namespace Modulith.Commerce.Products.Infrastructure.Configurations
{
    public class ProductVariantConfig : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariants");
            builder.HasKey(x => x.Id);


            builder.OwnsOne(x => x.Sku, name =>
            {
                name.Property(n => n.Value).HasColumnName("Sku").IsRequired();
                name.HasIndex(n => n.Value).IsUnique();
            });

            builder.OwnsOne(x => x.Barcode, name =>
            {
                name.Property(n => n.Value).HasColumnName("Barcode").IsRequired();
            });

            builder.OwnsOne(b => b.Price, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("Price_Amount")
                    .HasPrecision(18, 2)
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("Price_Currency")
                    .HasMaxLength(3)
                    .IsRequired()
                    .HasConversion(
                        currency => currency.Code,
                        code => Currency.GetFromCode(code)
                    );
            });

            builder.OwnsOne(b => b.Cost, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("Cost_Amount")
                    .HasPrecision(18, 2)
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("Cost_Currency")
                    .HasMaxLength(3)
                    .IsRequired()
                    .HasConversion(
                        currency => currency.Code,
                        code => Currency.GetFromCode(code)
                    );
            });

            builder.OwnsOne(b => b.DiscountCount, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("DiscountCount_Amount")
                    .HasPrecision(18, 2)
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("DiscountCount_Currency")
                    .HasMaxLength(3)
                    .IsRequired()
                    .HasConversion(
                        currency => currency.Code,
                        code => Currency.GetFromCode(code)
                    );
            });

            builder.OwnsOne(x => x.Stock, name =>
            {
                name.Property(n => n.Quantity).HasColumnName("Stock").IsRequired();
            });
        }
    }
}
