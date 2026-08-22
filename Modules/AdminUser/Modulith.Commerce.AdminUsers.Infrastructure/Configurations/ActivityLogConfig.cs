using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Commerce.AdminUser.Domain.ActivityLogs;
using DomainAction = Modulith.Commerce.AdminUser.Domain.Permissions.Action;
using DomainResource = Modulith.Commerce.AdminUser.Domain.Permissions.Resource;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Configurations
{
    public class ActivityLogConfig : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.ToTable("ActivityLogs");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Action)
                .HasConversion(a => a.Value, s => new DomainAction(s))
                .HasColumnName("Action")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Resource)
                .HasConversion(r => r.Value, s => new DomainResource(s))
                .HasColumnName("Resource")
                .IsRequired()
                .HasMaxLength(100);

            builder.OwnsOne(x => x.OldValue, ov =>
            {
                ov.Property(v => v.Value).HasColumnName("OldValue").IsRequired().HasColumnType("NVARCHAR(MAX)");
            });

            builder.OwnsOne(x => x.NewValue, nv =>
            {
                nv.Property(v => v.Value).HasColumnName("NewValue").IsRequired().HasColumnType("NVARCHAR(MAX)");
            });

            builder.OwnsOne(x => x.IpAddress, ip =>
            {
                ip.Property(v => v.Value).HasColumnName("IpAddress").IsRequired().HasMaxLength(50);
            });

            builder.OwnsOne(x => x.UserAgent, ua =>
            {
                ua.Property(v => v.Value).HasColumnName("UserAgent").IsRequired().HasMaxLength(500);
            });

            builder.OwnsOne(x => x.KeycloakSessionId, ks =>
            {
                ks.Property(v => v.Value).HasColumnName("KeycloakSessionId").IsRequired().HasMaxLength(200);
            });

            builder.OwnsOne(x => x.CorellationId, ci =>
            {
                ci.Property(v => v.Value).HasColumnName("CorellationId").IsRequired().HasMaxLength(200);
            });

            builder.Property(x => x.UserId).IsRequired();
        }
    }
}
