using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.Brands.Events
{
    public record UpdateBrandEvent(Guid Id) : IDomainEvent;
}
