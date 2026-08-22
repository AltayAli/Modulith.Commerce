using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.ProductVariantProperties.Commands.AddProductVariantProperties
{
    public record AddProductVariantPropertiesCommand : ICommand
    {
        public Guid VariantId { get; set; }
        public List<AddProductVariantPropertyItem> Properties { get; set; } = new();
    }

    public record AddProductVariantPropertyItem
    {
        public Guid CategoryPropertyId { get; set; }
        public string Value { get; set; }
    }
}
