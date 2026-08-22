using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.ProductVariants
{
    public class ProductVariantErrors
    {
        public static Error NullValue => new Error("ProductVariant.NullValue", "ProductVariant.NullValue");
        public static Error NotFound => new Error("ProductVariant.NotFound", "ProductVariant.NotFound");
        public static Error MaxLenght => new Error("ProductVariant.MaxLenght", "ProductVariant.MaxLenght");
    }
}
