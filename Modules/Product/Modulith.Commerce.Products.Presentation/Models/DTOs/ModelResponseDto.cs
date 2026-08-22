namespace Modulith.Commerce.Products.Presentation.Models.DTOs
{
    public record ModelResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
