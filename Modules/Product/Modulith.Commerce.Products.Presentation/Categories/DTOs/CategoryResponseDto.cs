namespace Modulith.Commerce.Products.Presentation.Categories.DTOs
{
    public record CategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Icon { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int SubCategoriesCount { get; set; }
    }
}
