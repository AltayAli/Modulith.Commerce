using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.ProductVariants;

namespace Modulith.Commerce.Products.Domain.ProductVariantImages
{
    public class ProductVariantImage : BaseEntity
    {
        private ProductVariantImage()
        {
        }

        public Guid ProductVariantId { get; private set; }
        public ProductVariant ProductVariant { get; private set; }
        public ImageUrl ImageUrl { get; private set; }
        public bool IsMain { get; private set; }

        public static ProductVariantImage Create(Guid productVariantId, string imageUrl, bool isMain)
        {
            return new ProductVariantImage
            {
                Id = Guid.NewGuid(),
                ProductVariantId = productVariantId,
                ImageUrl = new ImageUrl(imageUrl),
                IsMain = isMain
            };
        }
    }
}
