using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Products.Application.Caching;

namespace Modulith.Commerce.Products.Application.Products.Queries.GetProducts
{
    public record GetProductsQuery : ICacheQuery<List<GetProductsItemResponse>>
    {
        public string Key { get; set; } = string.Empty;

        public string CacheKey => CacheKeys.ProductsCacheKey;
        public TimeSpan? Expiration => TimeSpan.FromMinutes(30);
    }
}
