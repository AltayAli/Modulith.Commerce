namespace Modulith.Commerce.Products.Presentation.Brands.DTOs
{
    public record BrandResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int ModelsCount { get; set; }
    }
}
