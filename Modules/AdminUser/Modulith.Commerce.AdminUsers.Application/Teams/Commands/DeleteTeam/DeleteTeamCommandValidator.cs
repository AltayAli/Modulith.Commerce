using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.Teams;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommandValidator : AbstractValidator<DeleteTeamCommand>
    {
        public DeleteTeamCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(TeamErrors.MissingId.Code);
        }
    }
}
