using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.Brands.Commands.UpdateBrand
{
    public record UpdateBrandCommand : ICommand
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
    }
}
