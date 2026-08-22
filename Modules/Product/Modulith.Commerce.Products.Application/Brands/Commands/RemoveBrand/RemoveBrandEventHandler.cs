using MediatR;
using Modulith.Commerce.Common.Application.Caching;
using Modulith.Commerce.Products.Application.Caching;
using Modulith.Commerce.Products.Domain.Brands.Events;

namespace Modulith.Commerce.Products.Application.Brands.Commands.RemoveBrand
{
    public class RemoveBrandEventHandler
        (ICacheService cacheService)
        : INotificationHandler<RemoveBrandEvent>
    {
        public async Task Handle(RemoveBrandEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.BrandsCacheKey, cancellationToken);
        }
    }
}
