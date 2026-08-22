using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.Products.Commands.RemoveProduct
{
    public record RemoveProductCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
