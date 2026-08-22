using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.Models.Commands.UpdateModel
{
    public record UpdateModelCommand : ICommand
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required Guid BrandId { get; init; }
    }
}
