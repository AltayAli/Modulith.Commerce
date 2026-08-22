namespace Modulith.Commerce.Products.Presentation.Products.DTOs
{
    public record AddProductRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Slug { get; set; }
        public string? ShortDescription { get; set; }
        public Guid? ModelId { get; set; }
        public bool IsFeatured { get; set; }
        public string TaxClass { get; set; } = "standard";
        public SeoRequestDto? Seo { get; set; }
        public List<Guid> CategoryIds { get; set; } = new();
        public List<AddProductVariantRequestDto> Variants { get; set; } = new();
    }

    public record SeoRequestDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string> Keywords { get; set; } = new();
        public string? OgImage { get; set; }
    }
}
