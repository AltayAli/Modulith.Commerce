using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Departments.Queries.GetDepartment
{
    public record GetDepartmentQuery(Guid Id) : IQuery<GetDepartmentResponse?>;
}
