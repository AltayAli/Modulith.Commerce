using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.Categories.Commands.RemoveCategory
{
    public record RemoveCategoryCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
