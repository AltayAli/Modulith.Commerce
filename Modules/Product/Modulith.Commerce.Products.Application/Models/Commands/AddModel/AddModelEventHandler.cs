using MediatR;
using Modulith.Commerce.Common.Application.Caching;
using Modulith.Commerce.Products.Application.Caching;
using Modulith.Commerce.Products.Domain.Models.Events;

namespace Modulith.Commerce.Products.Application.Models.Commands.AddModel
{
    internal class AddModelEventHandler(ICacheService cacheService) : INotificationHandler<AddModelEvent>
    {
        public async Task Handle(AddModelEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.BrandsCacheKey, cancellationToken);
            await cacheService.RemoveAsync(CacheKeys.ModelsCacheKey, cancellationToken);
        }
    }
}
