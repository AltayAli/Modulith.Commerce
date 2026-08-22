using MediatR;
using Modulith.Commerce.Common.Application.Caching;
using Modulith.Commerce.Products.Application.Caching;
using Modulith.Commerce.Products.Domain.Categories.Events;

namespace Modulith.Commerce.Products.Application.Categories.Commands.AddCategory
{
    public class AddCategoryEventHandler
        (ICacheService cacheService)
        : INotificationHandler<AddCategoryEvent>
    {
        public async Task Handle(AddCategoryEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.BrandsCacheKey, cancellationToken);
        }
    }
}
