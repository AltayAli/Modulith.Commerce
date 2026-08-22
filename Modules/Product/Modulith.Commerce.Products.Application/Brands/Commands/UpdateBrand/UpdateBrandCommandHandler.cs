using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Brands;

namespace Modulith.Commerce.Products.Application.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandHandler
        (IBrandsRepository brandsRepository,
        IBrandExistenceChecker brandExistenceChecker,
        IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateBrandCommand>
    {
        public async Task<Result> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await brandsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Brand>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Brand, bool>>> {
                    m => m.Id == request.Id
                },
                IsLoadingAsNoTracking = false
            }, cancellationToken);

            if (brand is null)
            {
                return Result.Failure(BrandErrors.NotFound);
            }

            bool brandExistsViaChecker = await brandExistenceChecker.ExistsAsync(request.Name, cancellationToken);

            if (brandExistsViaChecker)
            {
                return Result.Failure(BrandErrors.AlreadyExists);
            }

            brand.Update(request.Name);

            await brandsRepository.UpdateAsync(brand);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
