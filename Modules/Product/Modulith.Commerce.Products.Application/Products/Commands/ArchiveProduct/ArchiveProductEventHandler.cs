using MediatR;
using Modulith.Commerce.Common.Application.Caching;
using Modulith.Commerce.Products.Application.Caching;
using Modulith.Commerce.Products.Domain.Products.Events;

namespace Modulith.Commerce.Products.Application.Products.Commands.ArchiveProduct
{
    public class ArchiveProductEventHandler(ICacheService cacheService)
        : INotificationHandler<ProductArchivedEvent>
    {
        public async Task Handle(ProductArchivedEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.ProductsCacheKey, cancellationToken);
            await cacheService.RemoveAsync(CacheKeys.ProductCacheKey(notification.Id), cancellationToken);
        }
    }
}
