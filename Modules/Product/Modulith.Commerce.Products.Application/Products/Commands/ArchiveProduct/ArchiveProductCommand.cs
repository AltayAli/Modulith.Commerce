using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.Products.Commands.ArchiveProduct
{
    public record ArchiveProductCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
