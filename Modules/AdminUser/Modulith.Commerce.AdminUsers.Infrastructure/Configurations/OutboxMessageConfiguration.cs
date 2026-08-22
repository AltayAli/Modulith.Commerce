using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Commerce.AdminUsers.Infrastructure.Outbox;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Configurations
{
    public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("AdminUserOutboxMessages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Content).HasColumnType("NVARCHAR(MAX)");
        }
    }
}
