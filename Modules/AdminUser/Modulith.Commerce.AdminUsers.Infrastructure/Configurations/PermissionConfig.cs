using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Commerce.AdminUser.Domain.Permissions;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Configurations
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");
            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => x.DeletedDate == null && x.DeletedById == null);

            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(n => n.Value).HasColumnName("Name").IsRequired().HasMaxLength(200);
            });

            builder.OwnsOne(x => x.Description, desc =>
            {
                desc.Property(d => d.Value).HasColumnName("Description").IsRequired().HasMaxLength(500);
            });

            builder.OwnsOne(x => x.Policy, policy =>
            {
                policy.Property(p => p.Value).HasColumnName("Policy").IsRequired().HasMaxLength(200);
                policy.HasIndex(p => p.Value).IsUnique();
            });

            builder.HasMany(x => x.RolePermissions)
                .WithOne(x => x.Permission)
                .HasForeignKey(x => x.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
