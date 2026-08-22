using FluentValidation;
using Modulith.Commerce.Products.Domain.Products;

namespace Modulith.Commerce.Products.Application.Products.Commands.ArchiveProduct
{
    public class ArchiveProductCommandValidator : AbstractValidator<ArchiveProductCommand>
    {
        public ArchiveProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithErrorCode(ProductErrors.NullValue.Code)
                .NotEqual(Guid.Empty)
                .WithErrorCode(ProductErrors.NullValue.Code);
        }
    }
}
