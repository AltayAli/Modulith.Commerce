using MediatR;
using Modulith.Commerce.Common.Application.Caching;
using Modulith.Commerce.Products.Application.Caching;
using Modulith.Commerce.Products.Domain.Products.Events;

namespace Modulith.Commerce.Products.Application.Products.Commands.CreateProduct
{
    public class CreateProductEventHandler(ICacheService cacheService)
        : INotificationHandler<ProductCreateEvent>
    {
        public async Task Handle(ProductCreateEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.ProductsCacheKey, cancellationToken);
            await cacheService.RemoveAsync(CacheKeys.ProductCacheKey(notification.Id), cancellationToken);
        }
    }
}
