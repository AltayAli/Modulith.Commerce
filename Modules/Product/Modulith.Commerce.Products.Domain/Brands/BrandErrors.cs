using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.Brands
{
    public static class BrandErrors
    {
        public static Error NullValue => new Error("Brand.NullValue", "Brand.NullValue");
        public static Error NotFound => new Error("Brand.NotFound", "Brand.NotFound");
        public static Error MaxLenght => new Error("Brand.MaxLenght", "Brand.MaxLenght");
        public static Error AlreadyExists => new Error("Brand.AlreadyExists", "Brand.AlreadyExists");
    }
}
