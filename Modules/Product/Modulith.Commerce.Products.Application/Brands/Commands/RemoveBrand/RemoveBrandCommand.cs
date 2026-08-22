using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.Brands.Commands.RemoveBrand
{
    public record RemoveBrandCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
