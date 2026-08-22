using FluentValidation;
using Modulith.Commerce.Products.Application.Products.Commands.CreateProduct;
using Modulith.Commerce.Products.Domain.Models;
using Modulith.Commerce.Products.Domain.Products;

namespace Modulith.Commerce.Products.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithErrorCode(ModelErrors.NullValue.Code)
                .NotEqual(Guid.Empty)
                .WithErrorCode(ModelErrors.NullValue.Code);

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(ProductErrors.NullValue.Code)
                .MaximumLength(100)
                .WithErrorCode(ProductErrors.MaxLenght.Code);

            RuleFor(x => x.Description)
                .MaximumLength(2000)
                .WithErrorCode(ProductErrors.MaxLenght.Code);

            RuleFor(x => x.ShortDescription)
                .MaximumLength(500)
                .WithErrorCode(ProductErrors.MaxLenght.Code);

            RuleFor(x => x.TaxClass)
                .NotEmpty()
                .MaximumLength(50)
                .WithErrorCode(ProductErrors.MaxLenght.Code);

            RuleFor(x => x.Slug).MaximumLength(280);

            RuleFor(x => x.Seo)
                .SetValidator(new SeoRequestValidator())
                .When(x => x.Seo is not null);
        }
    }
}
