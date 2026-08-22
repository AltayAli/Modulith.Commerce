using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Products.Application.Caching;

namespace Modulith.Commerce.Products.Application.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery : ICacheQuery<List<GetCategoriesItemResponse>>
    {
        public string Key { get; set; }
        public Guid? ParentId { get; set; }

        public string CacheKey => CacheKeys.CategoriesCacheKey;

        public TimeSpan? Expiration => TimeSpan.FromMinutes(40);
    }
}
