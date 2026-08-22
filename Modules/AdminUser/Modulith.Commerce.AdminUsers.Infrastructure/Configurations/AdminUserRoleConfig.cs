using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Commerce.AdminUser.Domain.AdminUserRoles;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Configurations
{
    public class AdminUserRoleConfig : IEntityTypeConfiguration<AdminUserRole>
    {
        public void Configure(EntityTypeBuilder<AdminUserRole> builder)
        {
            builder.ToTable("AdminUserRoles");
            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => x.DeletedDate == null && x.DeletedById == null);

            builder.OwnsOne(x => x.Reason, reason =>
            {
                reason.Property(r => r.Value).HasColumnName("Reason").HasMaxLength(500);
            });

            builder.Property(x => x.ExpiredAt).IsRequired(false);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
