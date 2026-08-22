using MediatR;
using Modulith.Commerce.Common.Application.Caching;
using Modulith.Commerce.Products.Application.Caching;
using Modulith.Commerce.Products.Domain.Brands.Events;

namespace Modulith.Commerce.Products.Application.Brands.Commands.AddBrand
{
    public class AddBrandEventHandler
        (ICacheService cacheService)
        : INotificationHandler<AddBrandEvent>
    {
        public async Task Handle(AddBrandEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.BrandsCacheKey, cancellationToken);

        }
    }
}
