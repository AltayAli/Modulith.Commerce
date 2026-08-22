using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.Teams;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.UpdateTeam
{
    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        public UpdateTeamCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(TeamErrors.MissingId.Code);
            RuleFor(x => x.Name).NotEmpty().WithMessage(TeamErrors.MissingName.Code);
            RuleFor(x => x.DepartmentId).NotEmpty().WithMessage(TeamErrors.MissingDepartmentId.Code);
            RuleFor(x => x.LeadId).NotEmpty().WithMessage(TeamErrors.MissingLeadId.Code);
        }
    }
}
