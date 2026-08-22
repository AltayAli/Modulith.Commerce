namespace Modulith.Commerce.AdminUsers.Application.Departments.Queries.GetDepartment
{
    public record GetDepartmentResponse(
        Guid Id,
        string Name,
        Guid? ParentId,
        Guid HeadId);
}
