namespace Modulith.Commerce.Products.Application.Caching
{
    public static class CacheKeys
    {
        public static string ProductsCacheKey => "products-cache-key";
        public static string BrandsCacheKey => "brands-cache-key";
        public static string ModelsCacheKey => "models-cache-key";
        public static string CategoriesCacheKey => "categories-cache-key";
        public static string CategoryPropertiesCacheKey => "category-properties-cache-key";

        public static string BrandCacheKey(Guid id) => $"brand-cache-key-{id}";
        public static string ProductCacheKey(Guid id) => $"product-cache-key-{id}";
    }
}
