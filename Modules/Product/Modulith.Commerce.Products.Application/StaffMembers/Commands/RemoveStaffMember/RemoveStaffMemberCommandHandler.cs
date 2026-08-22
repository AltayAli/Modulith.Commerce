using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.StaffMembers;

namespace Modulith.Commerce.Products.Application.StaffMembers.Commands.RemoveStaffMember
{
    public class RemoveStaffMemberCommandHandler(
        IStaffMembersRepository staffMembersRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<RemoveStaffMemberCommand>
    {
        public async Task<Result> Handle(RemoveStaffMemberCommand request, CancellationToken cancellationToken)
        {
            var staffMember = await staffMembersRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<StaffMember>
            {
                Predicates = [s => s.Id == request.AdminUserId]
            }, cancellationToken);

            if (staffMember is null)
                return Result.Success();

            await staffMembersRepository.DeleteAsync(staffMember, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
