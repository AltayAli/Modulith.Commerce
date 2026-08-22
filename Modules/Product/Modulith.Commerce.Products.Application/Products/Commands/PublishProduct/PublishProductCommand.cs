using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.Products.Commands.PublishProduct
{
    public record PublishProductCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
