namespace Modulith.Commerce.Products.Presentation.Products.DTOs
{
    public record AddProductVariantPropertiesRequestDto
    {
        public List<AddProductVariantPropertyItemDto> Properties { get; set; } = new();
    }

    public record AddProductVariantPropertyItemDto
    {
        public Guid CategoryPropertyId { get; set; }
        public string Value { get; set; }
    }
}
