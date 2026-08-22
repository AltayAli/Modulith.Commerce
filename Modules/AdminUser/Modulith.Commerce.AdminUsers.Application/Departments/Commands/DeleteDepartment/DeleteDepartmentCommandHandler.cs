using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.Departments;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommandHandler(
        IDepartmentsRepository departmentsRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<DeleteDepartmentCommand>
    {
        public async Task<Result> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await departmentsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Department>
            {
                Predicates = [d => d.Id == request.Id]
            }, cancellationToken);

            if (department is null)
                return Result.Failure(DepartmentErrors.NotFound);

            await departmentsRepository.DeleteAsync(department, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
