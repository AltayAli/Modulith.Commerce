namespace Modulith.Commerce.AdminUsers.Presentation.Departments.DTOs
{
    public record DepartmentDetailResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public Guid HeadId { get; set; }
    }
}
