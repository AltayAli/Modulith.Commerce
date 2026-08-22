using FluentValidation;
using Modulith.Commerce.Products.Domain.Products;

namespace Modulith.Commerce.Products.Application.Products.Commands.UnpublishProduct
{
    public class UnpublishProductCommandValidator : AbstractValidator<UnpublishProductCommand>
    {
        public UnpublishProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithErrorCode(ProductErrors.NullValue.Code)
                .NotEqual(Guid.Empty)
                .WithErrorCode(ProductErrors.NullValue.Code);
        }
    }
}
