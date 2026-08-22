using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.AdminUsers;
using Modulith.Commerce.AdminUser.Domain.Teams;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.TransferUserToTeam
{
    public class TransferUserToTeamCommandValidator : AbstractValidator<TransferUserToTeamCommand>
    {
        public TransferUserToTeamCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(AdminUserErrors.MissingId.Code);
            RuleFor(x => x.TeamId).NotEmpty().WithMessage(TeamErrors.MissingId.Code);
        }
    }
}
