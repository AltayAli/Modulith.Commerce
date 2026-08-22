namespace Modulith.Commerce.AdminUsers.Presentation.Departments.DTOs
{
    public record AddDepartmentRequestDto
    {
        public string Name { get; set; }
        public Guid HeadId { get; set; }
        public Guid? ParentId { get; set; }
    }
}
