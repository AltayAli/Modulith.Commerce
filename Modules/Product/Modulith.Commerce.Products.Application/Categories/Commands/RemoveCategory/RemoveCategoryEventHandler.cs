using MediatR;
using Modulith.Commerce.Common.Application.Caching;
using Modulith.Commerce.Products.Application.Caching;
using Modulith.Commerce.Products.Domain.Categories.Events;

namespace Modulith.Commerce.Products.Application.Categories.Commands.RemoveCategory
{
    public class RemoveCategoryEventHandler(ICacheService cacheService) : INotificationHandler<RemoveCategoryEvent>
    {
        public async Task Handle(RemoveCategoryEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.CategoriesCacheKey, cancellationToken);
        }
    }
}
