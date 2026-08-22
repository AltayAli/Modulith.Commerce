using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.Brands.Events
{
    public record AddBrandEvent(Brand Brand) : IDomainEvent;
}
