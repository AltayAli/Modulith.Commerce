namespace Modulith.Commerce.AdminUsers.Presentation.Teams.DTOs
{
    public record AddTeamRequestDto
    {
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid LeadId { get; set; }
    }
}
