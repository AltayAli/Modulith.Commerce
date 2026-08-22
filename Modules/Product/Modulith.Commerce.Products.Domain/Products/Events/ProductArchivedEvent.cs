using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.Products.Events
{
    public record ProductArchivedEvent(Guid Id) : IDomainEvent
    {
    }
}
