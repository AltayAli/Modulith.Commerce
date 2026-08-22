using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.ActivityLogs;

namespace Modulith.Commerce.AdminUsers.Application.ActivityLogs.Commands.AddActivityLog
{
    public class AddActivityLogCommandValidator : AbstractValidator<AddActivityLogCommand>
    {
        public AddActivityLogCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage(ActivityLogErrors.InvalidUserId.Code);

            RuleFor(x => x.Action)
                .NotEmpty()
                .WithMessage(ActivityLogErrors.InvalidAction.Code);

            RuleFor(x => x.Resource)
                .NotEmpty()
                .WithMessage(ActivityLogErrors.InvalidResource.Code);


            RuleFor(x => x.IpAddress)
                .NotEmpty()
                .WithMessage(ActivityLogErrors.InvalidIpAddress.Code);


        }
    }
}
