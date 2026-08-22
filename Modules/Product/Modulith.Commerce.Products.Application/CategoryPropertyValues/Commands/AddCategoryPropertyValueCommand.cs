using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.CategoryPropertyValues.Commands
{
    public record AddCategoryPropertyValueCommand : ICommand
    {
        public Guid PropertyId { get; set; }
        public List<string> Items { get; init; } = new();
    }
}
