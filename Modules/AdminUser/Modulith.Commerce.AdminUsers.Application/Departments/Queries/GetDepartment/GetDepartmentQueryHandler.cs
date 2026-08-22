using Modulith.Commerce.AdminUser.Domain.Departments;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.Departments.Queries.GetDepartment
{
    public class GetDepartmentQueryHandler(IDepartmentsRepository departmentsRepository)
        : IQueryHandler<GetDepartmentQuery, GetDepartmentResponse?>
    {
        public async Task<Result<GetDepartmentResponse?>> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            var department = await departmentsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Department>
            {
                Predicates = [d => d.Id == request.Id]
            }, cancellationToken);

            if (department is null)
                return Result.Failure<GetDepartmentResponse?>(null, DepartmentErrors.NotFound);

            return Result.Success<GetDepartmentResponse?>(new GetDepartmentResponse(
                department.Id,
                department.Name.Value,
                department.ParentId,
                department.HeadId));
        }
    }
}
