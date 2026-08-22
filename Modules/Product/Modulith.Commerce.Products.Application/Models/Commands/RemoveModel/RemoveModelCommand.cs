using Modulith.Commerce.Common.Application.Abstractions.Messaging;


namespace Modulith.Commerce.Products.Application.Models.Commands.RemoveModel
{
    public record RemoveModelCommand : ICommand
    {
        public Guid Id { get; init; }
    }
}
