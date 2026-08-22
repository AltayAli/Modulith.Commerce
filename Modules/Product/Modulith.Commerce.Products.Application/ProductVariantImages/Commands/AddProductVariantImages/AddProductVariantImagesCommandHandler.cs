using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.ProductVariantImages;
using Modulith.Commerce.Products.Domain.ProductVariants;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.ProductVariantImages.Commands.AddProductVariantImages
{
    public class AddProductVariantImagesCommandHandler(
        IProductVariantsRepository productVariantsRepository,
        IProductVariantImagesRepository productVariantImagesRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<AddProductVariantImagesCommand>
    {
        public async Task<Result> Handle(AddProductVariantImagesCommand request, CancellationToken cancellationToken)
        {
            bool variantExists = await productVariantsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<ProductVariant>
            {
                Predicates = new List<Expression<Func<ProductVariant, bool>>>
                {
                    v => v.Id == request.VariantId
                }
            }) is not null;

            if (!variantExists)
                return Result.Failure(ProductVariantErrors.NotFound);

            foreach (var item in request.Images)
            {
                var image = ProductVariantImage.Create(request.VariantId, item.ImageUrl, item.IsMain);
                await productVariantImagesRepository.InsertAsync(image, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
