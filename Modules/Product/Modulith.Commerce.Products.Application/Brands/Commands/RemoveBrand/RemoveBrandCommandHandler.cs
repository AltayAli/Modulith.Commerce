using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Brands;

namespace Modulith.Commerce.Products.Application.Brands.Commands.RemoveBrand
{
    public class RemoveBrandCommandHandler
        (IBrandsRepository brandsRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<RemoveBrandCommand>
    {
        public async Task<Result> Handle(RemoveBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await brandsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Brand>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Brand, bool>>> {
                    m => m.Id == request.Id
                },
                IsLoadingAsNoTracking = false
            }, cancellationToken);

            if (brand == null)
            {
                return Result.Failure(BrandErrors.NotFound);
            }

            brand.Remove();
            await brandsRepository.DeleteAsync(brand, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);


            return Result.Success();
        }
    }
}
