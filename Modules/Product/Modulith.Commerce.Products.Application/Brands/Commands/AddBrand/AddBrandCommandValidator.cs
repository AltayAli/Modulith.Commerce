using FluentValidation;
using Modulith.Commerce.Products.Domain.Brands;

namespace Modulith.Commerce.Products.Application.Brands.Commands.AddBrand
{
    public class AddBrandCommandValidator : AbstractValidator<AddBrandCommand>
    {
        public AddBrandCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(BrandErrors.NullValue.Code)
                .MaximumLength(100)
                .WithErrorCode(BrandErrors.MaxLenght.Code);
        }
    }
}
