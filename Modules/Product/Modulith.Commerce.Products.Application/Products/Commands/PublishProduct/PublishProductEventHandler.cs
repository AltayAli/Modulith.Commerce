using MediatR;
using Modulith.Commerce.Common.Application.Caching;
using Modulith.Commerce.Products.Application.Caching;
using Modulith.Commerce.Products.Domain.Products.Events;

namespace Modulith.Commerce.Products.Application.Products.Commands.PublishProduct
{
    public class PublishProductEventHandler(ICacheService cacheService)
        : INotificationHandler<ProductPublishedEvent>
    {
        public async Task Handle(ProductPublishedEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.ProductsCacheKey, cancellationToken);
            await cacheService.RemoveAsync(CacheKeys.ProductCacheKey(notification.Id), cancellationToken);
        }
    }
}
