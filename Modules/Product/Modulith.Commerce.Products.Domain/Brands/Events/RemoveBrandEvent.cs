using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.Brands.Events
{
    public record RemoveBrandEvent(Guid Id) : IDomainEvent;
}
