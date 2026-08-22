using MediatR;
using Modulith.Commerce.Common.Application.Caching;
using Modulith.Commerce.Products.Application.Caching;
using Modulith.Commerce.Products.Domain.Categories.Events;

namespace Modulith.Commerce.Products.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryEventHandler(ICacheService cacheService) : INotificationHandler<UpdateCategoryEvent>
    {
        public async Task Handle(UpdateCategoryEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.CategoriesCacheKey, cancellationToken);
        }
    }
}
