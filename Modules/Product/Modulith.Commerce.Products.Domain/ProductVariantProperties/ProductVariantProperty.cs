using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.CategoryProperties;
using Modulith.Commerce.Products.Domain.ProductVariants;

namespace Modulith.Commerce.Products.Domain.ProductVariantProperties
{
    public class ProductVariantProperty : BaseEntity
    {
        private ProductVariantProperty()
        {
        }

        public Guid ProductVariantId { get; private set; }
        public ProductVariant ProductVariant { get; private set; }
        public Guid CategoryPropertyId { get; private set; }
        public CategoryProperty CategoryProperty { get; private set; }
        public string Value { get; private set; }

        public static ProductVariantProperty Create(Guid productVariantId, Guid categoryPropertyId, string value)
        {
            return new ProductVariantProperty
            {
                Id = Guid.NewGuid(),
                ProductVariantId = productVariantId,
                CategoryPropertyId = categoryPropertyId,
                Value = value
            };
        }
    }
}
