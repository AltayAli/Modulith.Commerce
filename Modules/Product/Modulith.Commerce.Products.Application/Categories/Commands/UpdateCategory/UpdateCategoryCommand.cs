using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand : ICommand
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid? ParentId { get; set; }
        public required string Icon { get; set; }
    }
}
