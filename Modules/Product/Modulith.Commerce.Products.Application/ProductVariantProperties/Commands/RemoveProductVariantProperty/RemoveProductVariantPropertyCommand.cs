using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.ProductVariantProperties.Commands.RemoveProductVariantProperty
{
    public record RemoveProductVariantPropertyCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
