using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Brands;

namespace Modulith.Commerce.Products.Application.Brands.Commands.AddBrand
{
    public class AddBrandCommandHandler(
        IUnitOfWork unitOfWork,
        IBrandsRepository brandsRepository,
        IBrandExistenceChecker brandExistenceChecker)
        : ICommandHandler<AddBrandCommand>
    {
        public async Task<Result> Handle(AddBrandCommand request, CancellationToken cancellationToken)
        {
            bool brandExistsViaChecker = await brandExistenceChecker.ExistsAsync(request.Name, cancellationToken);

            if (brandExistsViaChecker)
            {
                return Result.Failure(BrandErrors.AlreadyExists);
            }

            var brand = Brand.Create(request.Name);

            await brandsRepository.InsertAsync(brand, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
