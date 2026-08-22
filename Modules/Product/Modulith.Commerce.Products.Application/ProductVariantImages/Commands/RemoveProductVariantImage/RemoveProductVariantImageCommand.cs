using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.ProductVariantImages.Commands.RemoveProductVariantImage
{
    public record RemoveProductVariantImageCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
