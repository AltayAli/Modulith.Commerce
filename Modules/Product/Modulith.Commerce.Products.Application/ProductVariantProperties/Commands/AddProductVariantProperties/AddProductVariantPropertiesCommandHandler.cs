using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.ProductVariantProperties;
using Modulith.Commerce.Products.Domain.ProductVariants;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.ProductVariantProperties.Commands.AddProductVariantProperties
{
    public class AddProductVariantPropertiesCommandHandler(
        IProductVariantsRepository productVariantsRepository,
        IProductVariantPropertiesRepository productVariantPropertiesRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<AddProductVariantPropertiesCommand>
    {
        public async Task<Result> Handle(AddProductVariantPropertiesCommand request, CancellationToken cancellationToken)
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

            foreach (var item in request.Properties)
            {
                var property = ProductVariantProperty.Create(request.VariantId, item.CategoryPropertyId, item.Value);
                await productVariantPropertiesRepository.InsertAsync(property, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
