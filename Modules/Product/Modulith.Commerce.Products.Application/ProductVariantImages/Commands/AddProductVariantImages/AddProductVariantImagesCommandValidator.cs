using FluentValidation;

namespace Modulith.Commerce.Products.Application.ProductVariantImages.Commands.AddProductVariantImages
{
    public class AddProductVariantImagesCommandValidator : AbstractValidator<AddProductVariantImagesCommand>
    {
        public AddProductVariantImagesCommandValidator()
        {
            RuleFor(x => x.VariantId).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Images).NotEmpty();
            RuleForEach(x => x.Images).SetValidator(new AddProductVariantImageItemValidator());
        }
    }

    public class AddProductVariantImageItemValidator : AbstractValidator<AddProductVariantImageItem>
    {
        public AddProductVariantImageItemValidator()
        {
            RuleFor(x => x.ImageUrl).NotEmpty().MaximumLength(2048);
        }
    }
}
