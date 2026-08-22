using FluentValidation;
using Modulith.Commerce.Products.Domain.Models;

namespace Modulith.Commerce.Products.Application.Models.Commands.UpdateModel
{
    public class UpdateModelCommandValidator : AbstractValidator<UpdateModelCommand>
    {
        public UpdateModelCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithErrorCode(ModelErrors.NullValue.Code)
                .NotEqual(Guid.Empty)
                .WithErrorCode(ModelErrors.NullValue.Code);

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(ModelErrors.NullValue.Code)
                .MaximumLength(100)
                .WithErrorCode(ModelErrors.MaxLenght.Code);

            RuleFor(x => x.BrandId)
                .NotEmpty()
                .WithErrorCode(ModelErrors.NullValue.Code)
                .NotEqual(Guid.Empty)
                .WithErrorCode(ModelErrors.NullValue.Code);
        }
    }
}
