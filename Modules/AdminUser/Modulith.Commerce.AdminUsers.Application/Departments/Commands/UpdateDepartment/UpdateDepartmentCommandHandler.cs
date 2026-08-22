using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.Departments;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler(
        IDepartmentsRepository departmentsRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateDepartmentCommand>
    {
        public async Task<Result> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await departmentsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Department>
            {
                Predicates = [d => d.Id == request.Id]
            }, cancellationToken);

            if (department is null)
                return Result.Failure(DepartmentErrors.NotFound);

            string trimmedName = request.Name.Trim().ToLower();

            bool nameExists = await departmentsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Department>
            {
                Predicates = [d => d.Name.Value.ToLower() == trimmedName && d.Id != request.Id]
            }, cancellationToken) is not null;

            if (nameExists)
                return Result.Failure(DepartmentErrors.AlreadyExists);

            department.Update(request.Name, request.HeadId, request.ParentId);
            await departmentsRepository.UpdateAsync(department, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
