using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Products.Domain.CategoryProperties;

namespace Modulith.Commerce.Products.Application.CategoryProperties.Commands.AddCategoryProperty
{
    public record AddCategoryPropertyCommand : ICommand<List<Guid>>
    {
        public AddCategoryPropertyCommand()
        {
            Items = new List<AddCategoryPropertyCommandItem>();
        }
        public Guid CategoryId { get; set; }
        public List<AddCategoryPropertyCommandItem> Items { get; set; }
    }

    public record AddCategoryPropertyCommandItem
    {
        public required string Name { get; set; }
        public CategoryPropertyType Type { get; set; }
        public bool AddToFilter { get; set; }
        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
        public List<string> Values { get; set; }
    }
}
