using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.Products
{
    public class ProductErrors
    {
        public static Error NullValue => new Error("Product.NullValue", "Product.NullValue");
        public static Error NotFound => new Error("Product.NotFound", "Product.NotFound");
        public static Error MaxLenght => new Error("Product.MaxLenght", "Product.MaxLenght");
        public static Error SlugAlreadyExists => new Error("Product.SlugAlreadyExists", "Product.SlugAlreadyExists");
        public static Error CannotPublishWithoutActiveVariant => new Error("Product.CannotPublishWithoutActiveVariant", "Product.CannotPublishWithoutActiveVariant");
    }
}
