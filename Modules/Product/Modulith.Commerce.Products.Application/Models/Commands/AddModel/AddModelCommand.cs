using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.Models.Commands.AddModel
{
    public record AddModelCommand : ICommand
    {
        public required string Name { get; init; }
        public required Guid BrandId { get; init; }
    }
}
