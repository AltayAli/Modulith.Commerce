using FluentValidation;

namespace Modulith.Commerce.Products.Application.StaffMembers.Commands.RemoveStaffMember
{
    public class RemoveStaffMemberCommandValidator : AbstractValidator<RemoveStaffMemberCommand>
    {
        public RemoveStaffMemberCommandValidator()
        {
            RuleFor(x => x.AdminUserId).NotEmpty();
        }
    }
}
