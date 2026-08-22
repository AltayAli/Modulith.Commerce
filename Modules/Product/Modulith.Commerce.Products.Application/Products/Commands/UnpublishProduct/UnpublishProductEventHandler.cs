using MediatR;
using Modulith.Commerce.Common.Application.Caching;
using Modulith.Commerce.Products.Application.Caching;
using Modulith.Commerce.Products.Domain.Products.Events;

namespace Modulith.Commerce.Products.Application.Products.Commands.UnpublishProduct
{
    public class UnpublishProductEventHandler(ICacheService cacheService)
        : INotificationHandler<ProductUnpublishedEvent>
    {
        public async Task Handle(ProductUnpublishedEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.ProductsCacheKey, cancellationToken);
            await cacheService.RemoveAsync(CacheKeys.ProductCacheKey(notification.Id), cancellationToken);
        }
    }
}
