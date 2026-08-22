using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Products.Application.Caching;

namespace Modulith.Commerce.Products.Application.Brands.Queries.GetBrands
{
    public record GetBrandsQuery : ICacheQuery<List<GetBrandsResponse>>
    {
        public string Key { get; set; }

        public string CacheKey => CacheKeys.BrandsCacheKey;

        public TimeSpan? Expiration => TimeSpan.FromDays(1);
    }
}
