using MediatR;
using Modulith.Commerce.Common.Application.Caching;
using Modulith.Commerce.Products.Application.Caching;
using Modulith.Commerce.Products.Domain.Models.Events;

namespace Modulith.Commerce.Products.Application.Models.Commands.UpdateModel
{
    public class UpdateModelEventHandler(ICacheService cacheService) : INotificationHandler<UpdateModelEvent>
    {
        public async Task Handle(UpdateModelEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.ModelsCacheKey);
        }
    }
}
