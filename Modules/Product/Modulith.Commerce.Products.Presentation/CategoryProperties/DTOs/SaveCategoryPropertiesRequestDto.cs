using Modulith.Commerce.Products.Domain.CategoryProperties;

namespace Modulith.Commerce.Products.Presentation.CategoryProperties.DTOs
{
    public record SaveCategoryPropertiesRequestDto
    {
        public List<SaveCategoryPropertyItemDto> Items { get; set; } = new();
    }

    public record SaveCategoryPropertyItemDto
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public CategoryPropertyType Type { get; set; }
        public bool AddToFilter { get; set; }
        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
        public List<SaveCategoryPropertyValueDto> Values { get; set; } = new();
    }

    public record SaveCategoryPropertyValueDto
    {
        public Guid? Id { get; set; }
        public string Value { get; set; }
    }
}
