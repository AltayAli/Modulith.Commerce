using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Products.Application.Caching;

namespace Modulith.Commerce.Products.Application.Products.Queries.GetProduct
{
    public record GetProductQuery : ICacheQuery<GetProductResponse>
    {
        public Guid Id { get; set; }

        public string CacheKey => CacheKeys.ProductCacheKey(Id);
        public TimeSpan? Expiration => TimeSpan.FromMinutes(30);
    }
}
