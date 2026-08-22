using MediatR;
using Modulith.Commerce.Common.Application.Caching;
using Modulith.Commerce.Products.Application.Caching;
using Modulith.Commerce.Products.Domain.Products.Events;

namespace Modulith.Commerce.Products.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductEventHandler
        (ICacheService cacheService)
        : INotificationHandler<ProductUpdateEvent>
    {
        public async Task Handle(ProductUpdateEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.ProductsCacheKey, cancellationToken);
            await cacheService.RemoveAsync(CacheKeys.ProductCacheKey(notification.Id), cancellationToken);
        }
    }
}
