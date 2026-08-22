using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Brands;
using Modulith.Commerce.Products.Domain.Models;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.Models.Commands.UpdateModel
{
    public class UpdateModelCommandHandler(IModelsRepository modelsRepository,
                                IBrandsRepository brandsRepository,
                                IModelExistenceChecker modelExistenceChecker,
                                IUnitOfWork unitOfWork) : ICommandHandler<UpdateModelCommand>
    {
        public async Task<Result> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
        {

            bool brandExists = await brandsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Brand>
            {
                Predicates = new List<Expression<Func<Brand, bool>>>
                {
                    brand => brand.Id == request.BrandId
                },
            }, cancellationToken) is not null;

            if (brandExists)
            {
                return Result.Failure(BrandErrors.NotFound);
            }


            var modelExistsViaChecker = await modelExistenceChecker.ExistsAsync(request.Name, cancellationToken);

            if (modelExistsViaChecker)
            {
                return Result.Failure(ModelErrors.AlreadyExists);
            }

            Model? model = await modelsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Model>
            {
                Predicates = new List<Expression<Func<Model, bool>>>
                {
                    model => model.Id == request.Id
                },
            }, cancellationToken);

            if (model is null)
            {
                return Result.Failure(ModelErrors.NotFound);
            }

            model.Update(request.Name, request.BrandId);

            await modelsRepository.UpdateAsync(model, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
