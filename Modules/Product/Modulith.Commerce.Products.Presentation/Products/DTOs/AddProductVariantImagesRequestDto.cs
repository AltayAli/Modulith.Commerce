namespace Modulith.Commerce.Products.Presentation.Products.DTOs
{
    public record AddProductVariantImagesRequestDto
    {
        public List<AddProductVariantImageItemDto> Images { get; set; } = new();
    }

    public record AddProductVariantImageItemDto
    {
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }
    }
}
