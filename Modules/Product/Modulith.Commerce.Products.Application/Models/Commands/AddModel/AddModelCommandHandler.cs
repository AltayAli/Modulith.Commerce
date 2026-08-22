using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Brands;
using Modulith.Commerce.Products.Domain.Models;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.Models.Commands.AddModel
{
    public class AddModelCommandHandler
        (IBrandsRepository brandsRepository,
        IModelsRepository modelsRepository,
        IModelExistenceChecker modelExistenceChecker,
        IUnitOfWork unitOfWork)
        : ICommandHandler<AddModelCommand>
    {
        public async Task<Result> Handle(AddModelCommand request, CancellationToken cancellationToken)
        {
            bool brandExists = brandsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Brand>
            {
                Predicates = new List<Expression<Func<Brand, bool>>>
                {
                    brand => brand.Id == request.BrandId
                },
            }) is not null;

            if (brandExists)
            {
                return Result.Failure(BrandErrors.NotFound);
            }

            var modelExistsViaChecker = await modelExistenceChecker.ExistsAsync(request.Name, cancellationToken);

            if (modelExistsViaChecker)
            {
                return Result.Failure(ModelErrors.AlreadyExists);
            }

            var model = Model.Create(request.Name, request.BrandId);

            await modelsRepository.InsertAsync(model);
            await unitOfWork.SaveChangesAsync();

            return Result.Success();

        }
    }
}
