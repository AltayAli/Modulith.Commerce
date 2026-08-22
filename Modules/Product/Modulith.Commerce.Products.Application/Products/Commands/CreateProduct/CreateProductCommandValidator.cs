using FluentValidation;

namespace Modulith.Commerce.Products.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
            RuleFor(x => x.ShortDescription).MaximumLength(500);
            RuleFor(x => x.TaxClass).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Slug).MaximumLength(280);

            RuleFor(x => x.Seo)
                .SetValidator(new SeoRequestValidator())
                .When(x => x.Seo is not null);
        }
    }

    public class SeoRequestValidator : AbstractValidator<SeoRequest>
    {
        public SeoRequestValidator()
        {
            RuleFor(x => x.Title).MaximumLength(200);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.OgImage).MaximumLength(2048);
            RuleFor(x => x.Keywords).Must(k => k.Count <= 20)
                .WithMessage("Keywords cannot contain more than 20 items.");
        }
    }
}
