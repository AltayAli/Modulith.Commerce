namespace Modulith.Commerce.AdminUsers.Presentation.Departments.DTOs
{
    public record DepartmentListItemResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Parent { get; set; }
        public string Head { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
