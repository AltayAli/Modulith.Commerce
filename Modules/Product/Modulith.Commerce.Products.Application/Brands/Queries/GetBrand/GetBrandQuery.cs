using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Products.Application.Caching;

namespace Modulith.Commerce.Products.Application.Brands.Queries.GetBrand
{
    public class GetBrandQuery : ICacheQuery<GetBrandResponse>
    {
        public Guid Id { get; set; }

        public string CacheKey => CacheKeys.BrandCacheKey(Id);

        public TimeSpan? Expiration => TimeSpan.FromDays(1);
    }
}
