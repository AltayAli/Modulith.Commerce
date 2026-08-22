using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.ProductVariants.Commands.RemoveProductVariant
{
    public record RemoveProductVariantCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
