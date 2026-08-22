namespace Modulith.Commerce.Products.Presentation.Authorization
{

    public static class ProductPolicies
    {
        public const string CategoryRead = "category:read";
        public const string CategoryWrite = "category:write";
        public const string CategoryDelete = "category:delete";

        public const string CategoryPropertyRead = "categoryproperty:read";
        public const string CategoryPropertyWrite = "categoryproperty:write";

        public const string ProductVariantWrite = "productvariant:write";
        public const string ProductVariantDelete = "productvariant:delete";

        public const string BrandRead = "brand:read";
        public const string BrandWrite = "brand:write";
        public const string BrandDelete = "brand:delete";

        public const string ModelRead = "model:read";
        public const string ModelWrite = "model:write";
        public const string ModelDelete = "model:delete";

        public const string ProductRead = "product:read";
        public const string ProductWrite = "product:write";
        public const string ProductDelete = "product:delete";
    }
}
