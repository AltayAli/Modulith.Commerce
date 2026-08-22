using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Domain.ValueObjects;
using Modulith.Commerce.Products.Domain.CategoryPropertyValues;
using Modulith.Commerce.Products.Domain.Products;
using Modulith.Commerce.Products.Domain.ProductVariantImages;
using Modulith.Commerce.Products.Domain.ProductVariantProperties;

namespace Modulith.Commerce.Products.Domain.ProductVariants
{
    public class ProductVariant : BaseEntity
    {
        private ProductVariant()
        {
            CategoryPropertyValues = new HashSet<CategoryPropertyValue>();
            Images = new HashSet<ProductVariantImage>();
            Properties = new HashSet<ProductVariantProperty>();
        }

        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public Sku Sku { get; private set; }
        public Barcode Barcode { get; private set; }
        public Money Price { get; private set; }
        public Money? Cost { get; private set; }
        public decimal? TaxRate { get; private set; }
        public Stock Stock { get; private set; }
        public bool IsDefault { get; private set; }
        public bool IsActive { get; private set; }
        public Money DiscountCount { get; private set; }
        public DateTime? DiscountStartDate { get; private set; }
        public DateTime? DiscountEndDate { get; private set; }
        public HashSet<ProductVariantImage> Images { get; private set; }
        public HashSet<CategoryPropertyValue> CategoryPropertyValues { get; private set; }
        public HashSet<ProductVariantProperty> Properties { get; private set; }

        public ProductVariant Update(
            string sku,
            string barcode,
            Money price,
            Money? cost,
            decimal? taxRate,
            int stockQuantity,
            bool isDefault,
            bool isActive,
            Money discountCount,
            DateTime? discountStartDate,
            DateTime? discountEndDate)
        {
            Sku = new Sku(sku);
            Barcode = new Barcode(barcode);
            Price = price;
            Cost = cost;
            TaxRate = taxRate;
            Stock = new Stock(stockQuantity);
            IsDefault = isDefault;
            IsActive = isActive;
            DiscountCount = discountCount;
            DiscountStartDate = discountStartDate;
            DiscountEndDate = discountEndDate;
            return this;
        }

        public static ProductVariant Create(
            Guid productId,
            string sku,
            string barcode,
            Money price,
            Money? cost,
            decimal? taxRate,
            int stockQuantity,
            bool isDefault,
            bool isActive,
            Money discountCount,
            DateTime? discountStartDate,
            DateTime? discountEndDate)
        {
            return new ProductVariant
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                Sku = new Sku(sku),
                Barcode = new Barcode(barcode),
                Price = price,
                Cost = cost,
                TaxRate = taxRate,
                Stock = new Stock(stockQuantity),
                IsDefault = isDefault,
                IsActive = isActive,
                DiscountCount = discountCount,
                DiscountStartDate = discountStartDate,
                DiscountEndDate = discountEndDate
            };
        }
    }
}
