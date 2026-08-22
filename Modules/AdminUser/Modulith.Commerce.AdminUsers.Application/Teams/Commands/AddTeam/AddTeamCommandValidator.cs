using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.Teams;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.AddTeam
{
    public class AddTeamCommandValidator : AbstractValidator<AddTeamCommand>
    {
        public AddTeamCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(TeamErrors.MissingName.Code);
            RuleFor(x => x.DepartmentId).NotEmpty().WithMessage(TeamErrors.MissingDepartmentId.Code);
            RuleFor(x => x.LeadId).NotEmpty().WithMessage(TeamErrors.MissingLeadId.Code);
        }
    }
}
