using FluentValidation;
using Modulith.Commerce.Products.Domain.Models;

namespace Modulith.Commerce.Products.Application.Products.Commands.RemoveProduct
{
    public class RemoveProductCommandValidator : AbstractValidator<RemoveProductCommand>
    {
        public RemoveProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithErrorCode(ModelErrors.NullValue.Code)
                .NotEqual(Guid.Empty)
                .WithErrorCode(ModelErrors.NullValue.Code);
        }
    }
}
