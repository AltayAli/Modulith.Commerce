using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.AdminUsers;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.RemoveUserFromTeam
{
    public class RemoveUserFromTeamCommandValidator : AbstractValidator<RemoveUserFromTeamCommand>
    {
        public RemoveUserFromTeamCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(AdminUserErrors.MissingId.Code);
        }
    }
}
