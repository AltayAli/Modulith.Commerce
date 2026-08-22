using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.StaffMembers;

namespace Modulith.Commerce.Products.Application.StaffMembers.Commands.UpsertStaffMember
{
    public class UpsertStaffMemberCommandHandler(
        IStaffMembersRepository staffMembersRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<UpsertStaffMemberCommand>
    {
        public async Task<Result> Handle(UpsertStaffMemberCommand request, CancellationToken cancellationToken)
        {
            var staffMember = await staffMembersRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<StaffMember>
            {
                Predicates = [s => s.Id == request.AdminUserId]
            }, cancellationToken);

            if (staffMember is null)
            {
                staffMember = StaffMember.CreateFrom(request.AdminUserId, request.Email, request.FullName, request.Status, request.TeamId);

                await staffMembersRepository.InsertAsync(staffMember, cancellationToken);
            }
            else
            {
                staffMember.ApplyUpdate(request.Email, request.FullName, request.Status, request.TeamId);

                await staffMembersRepository.UpdateAsync(staffMember, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
