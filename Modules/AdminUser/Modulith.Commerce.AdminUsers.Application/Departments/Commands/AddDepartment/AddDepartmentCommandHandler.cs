using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.Departments;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.Departments.Commands.AddDepartment
{
    public class AddDepartmentCommandHandler(
        IDepartmentsRepository departmentsRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<AddDepartmentCommand>
    {
        public async Task<Result> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            string trimmedName = request.Name.Trim().ToLower();

            bool nameExists = await departmentsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Department>
            {
                Predicates = [d => d.Name.Value.ToLower() == trimmedName]
            }, cancellationToken) is not null;

            if (nameExists)
                return Result.Failure(DepartmentErrors.AlreadyExists);

            var department = Department.Create(request.Name, request.HeadId, request.ParentId);
            await departmentsRepository.InsertAsync(department, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
