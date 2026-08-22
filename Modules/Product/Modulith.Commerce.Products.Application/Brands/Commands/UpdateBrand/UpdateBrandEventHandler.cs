using MediatR;
using Modulith.Commerce.Common.Application.Caching;
using Modulith.Commerce.Products.Application.Caching;
using Modulith.Commerce.Products.Domain.Brands.Events;

namespace Modulith.Commerce.Products.Application.Brands.Commands.UpdateBrand
{
    public class UpdateBrandEventHandler
        (ICacheService cacheService)
        : INotificationHandler<UpdateBrandEvent>
    {
        public async Task Handle(UpdateBrandEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.BrandsCacheKey, cancellationToken);
        }
    }
}
