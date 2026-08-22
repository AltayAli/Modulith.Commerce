namespace Modulith.Commerce.AdminUsers.Presentation.Departments.DTOs
{
    public record UpdateDepartmentRequestDto
    {
        public string Name { get; set; }
        public Guid HeadId { get; set; }
        public Guid? ParentId { get; set; }
    }
}
