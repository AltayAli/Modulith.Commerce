using FluentValidation;
using Modulith.Commerce.Products.Domain.Brands;

namespace Modulith.Commerce.Products.Application.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(BrandErrors.NullValue.Code)
                .MaximumLength(100)
                .WithErrorCode(BrandErrors.MaxLenght.Code);
        }
    }
}
