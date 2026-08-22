using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Departments.Queries.GetDepartments
{
    public record GetDepartmentsQuery(string? Key) : IQuery<List<GetDepartmentsItemResponse>>;
}
