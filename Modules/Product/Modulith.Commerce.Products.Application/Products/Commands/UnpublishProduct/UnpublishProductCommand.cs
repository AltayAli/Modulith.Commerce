using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.Products.Commands.UnpublishProduct
{
    public record UnpublishProductCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
