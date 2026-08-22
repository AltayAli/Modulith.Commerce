namespace Modulith.Commerce.AdminUsers.Application.Departments.Queries.GetDepartments
{
    public record GetDepartmentsItemResponse(
        Guid Id,
        string Name,
        string? Parent,
        string Head,
        DateTime? LastModifiedDate);
}
