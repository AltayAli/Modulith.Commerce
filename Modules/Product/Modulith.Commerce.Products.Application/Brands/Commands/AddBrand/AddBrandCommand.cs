using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.Brands.Commands.AddBrand
{
    public record AddBrandCommand : ICommand
    {
        public required string Name { get; init; }
    }
}
