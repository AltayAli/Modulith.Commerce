using Modulith.Commerce.Products.Domain.CategoryProperties;

namespace Modulith.Commerce.Products.Presentation.CategoryProperties.DTOs
{
    public record CategoryPropertyResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CategoryPropertyType Type { get; set; }
        public bool AddToFilter { get; set; }
        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
        public int ValuesCount { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
