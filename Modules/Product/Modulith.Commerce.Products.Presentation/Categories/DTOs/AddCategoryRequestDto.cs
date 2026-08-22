namespace Modulith.Commerce.Products.Presentation.Categories.DTOs
{
    public record AddCategoryRequestDto
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public Guid? ParentId { get; set; }
    }
}
