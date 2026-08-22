using FluentValidation;

namespace Modulith.Commerce.Products.Application.StaffMembers.Commands.UpsertStaffMember
{
    public class UpsertStaffMemberCommandValidator : AbstractValidator<UpsertStaffMemberCommand>
    {
        public UpsertStaffMemberCommandValidator()
        {
            RuleFor(x => x.AdminUserId).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().MaximumLength(300);
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Status).NotEmpty().MaximumLength(50);
        }
    }
}
