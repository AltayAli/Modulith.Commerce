
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modulith.Commerce.AdminUsers.Infrastructure.Data;

#nullable disable

namespace Modulith.Commerce.AdminUsers.Infrastructure.Migrations
{
    [DbContext(typeof(AdminUsersDbContext))]
    [Migration("20260731205318_RemovePermissionResourceAction_AddPolicy")]
    partial class RemovePermissionResourceAction_AddPolicy
    {
                protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("adminusers")
                .HasAnnotation("ProductVersion", "10.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.ActivityLogs.ActivityLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Action");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Resource")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Resource");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("ActivityLogs", "adminusers");
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.AdminUserRoles.AdminUserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExpiredAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AdminUserRoles", "adminusers");
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ContractEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ContractStartDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("KeyCloakId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("MfaEnabled")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("OffboardedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("AdminUsers", "adminusers");
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.Departments.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("AdminUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("HeadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AdminUserId");

                    b.HasIndex("HeadId");

                    b.HasIndex("ParentId");

                    b.ToTable("Departments", "adminusers");
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.Permissions.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Permissions", "adminusers");
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.RolePermissions.RolePermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId", "PermissionId")
                        .IsUnique();

                    b.ToTable("RolePermissions", "adminusers");
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.Roles.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsSystemRole")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Roles", "adminusers");
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.Teams.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("AdminUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LeadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AdminUserId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("LeadId");

                    b.ToTable("Teams", "adminusers");
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUsers.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Occured")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Processed")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AdminUserOutboxMessages", "adminusers");
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.ActivityLogs.ActivityLog", b =>
                {
                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "CorellationId", b1 =>
                        {
                            b1.Property<Guid>("ActivityLogId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("CorellationId");

                            b1.HasKey("ActivityLogId");

                            b1.ToTable("ActivityLogs", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("ActivityLogId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "IpAddress", b1 =>
                        {
                            b1.Property<Guid>("ActivityLogId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("IpAddress");

                            b1.HasKey("ActivityLogId");

                            b1.ToTable("ActivityLogs", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("ActivityLogId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "KeycloakSessionId", b1 =>
                        {
                            b1.Property<Guid>("ActivityLogId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("KeycloakSessionId");

                            b1.HasKey("ActivityLogId");

                            b1.ToTable("ActivityLogs", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("ActivityLogId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "NewValue", b1 =>
                        {
                            b1.Property<Guid>("ActivityLogId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("NVARCHAR(MAX)")
                                .HasColumnName("NewValue");

                            b1.HasKey("ActivityLogId");

                            b1.ToTable("ActivityLogs", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("ActivityLogId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "OldValue", b1 =>
                        {
                            b1.Property<Guid>("ActivityLogId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("NVARCHAR(MAX)")
                                .HasColumnName("OldValue");

                            b1.HasKey("ActivityLogId");

                            b1.ToTable("ActivityLogs", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("ActivityLogId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "UserAgent", b1 =>
                        {
                            b1.Property<Guid>("ActivityLogId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("UserAgent");

                            b1.HasKey("ActivityLogId");

                            b1.ToTable("ActivityLogs", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("ActivityLogId");
                        });

                    b.Navigation("CorellationId")
                        .IsRequired();

                    b.Navigation("IpAddress")
                        .IsRequired();

                    b.Navigation("KeycloakSessionId")
                        .IsRequired();

                    b.Navigation("NewValue")
                        .IsRequired();

                    b.Navigation("OldValue")
                        .IsRequired();

                    b.Navigation("UserAgent")
                        .IsRequired();
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.AdminUserRoles.AdminUserRole", b =>
                {
                    b.HasOne("Modulith.Commerce.AdminUser.Domain.Roles.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "Reason", b1 =>
                        {
                            b1.Property<Guid>("AdminUserRoleId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("Reason");

                            b1.HasKey("AdminUserRoleId");

                            b1.ToTable("AdminUserRoles", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("AdminUserRoleId");
                        });

                    b.Navigation("Reason");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser", b =>
                {
                    b.HasOne("Modulith.Commerce.AdminUser.Domain.Teams.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "FirstName", b1 =>
                        {
                            b1.Property<Guid>("AdminUserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("FirstName");

                            b1.HasKey("AdminUserId");

                            b1.ToTable("AdminUsers", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("AdminUserId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "LastName", b1 =>
                        {
                            b1.Property<Guid>("AdminUserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("LastName");

                            b1.HasKey("AdminUserId");

                            b1.ToTable("AdminUsers", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("AdminUserId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "Title", b1 =>
                        {
                            b1.Property<Guid>("AdminUserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Title");

                            b1.HasKey("AdminUserId");

                            b1.ToTable("AdminUsers", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("AdminUserId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("AdminUserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(300)
                                .HasColumnType("nvarchar(300)")
                                .HasColumnName("Email");

                            b1.HasKey("AdminUserId");

                            b1.ToTable("AdminUsers", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("AdminUserId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.FileUrl", "AvatarUrl", b1 =>
                        {
                            b1.Property<Guid>("AdminUserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .HasColumnType("nvarchar(1000)")
                                .HasColumnName("AvatarUrl");

                            b1.HasKey("AdminUserId");

                            b1.ToTable("AdminUsers", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("AdminUserId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("AdminUserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("AdminUserId");

                            b1.ToTable("AdminUsers", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("AdminUserId");

                            b1.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.PhoneNumberCountryCode", "CountryCode", b2 =>
                                {
                                    b2.Property<Guid>("PhoneNumberAdminUserId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Code")
                                        .IsRequired()
                                        .HasMaxLength(10)
                                        .HasColumnType("nvarchar(10)")
                                        .HasColumnName("PhoneCountryCode");

                                    b2.HasKey("PhoneNumberAdminUserId");

                                    b2.ToTable("AdminUsers", "adminusers");

                                    b2.WithOwner()
                                        .HasForeignKey("PhoneNumberAdminUserId");
                                });

                            b1.Navigation("CountryCode")
                                .IsRequired();
                        });

                    b.Navigation("AvatarUrl");

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("FirstName")
                        .IsRequired();

                    b.Navigation("LastName")
                        .IsRequired();

                    b.Navigation("PhoneNumber");

                    b.Navigation("Team");

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.Departments.Department", b =>
                {
                    b.HasOne("Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser", null)
                        .WithMany("Departments")
                        .HasForeignKey("AdminUserId");

                    b.HasOne("Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser", "Head")
                        .WithMany()
                        .HasForeignKey("HeadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Modulith.Commerce.AdminUser.Domain.Departments.Department", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "Name", b1 =>
                        {
                            b1.Property<Guid>("DepartmentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Name");

                            b1.HasKey("DepartmentId");

                            b1.ToTable("Departments", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("DepartmentId");
                        });

                    b.Navigation("Head");

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.Permissions.Permission", b =>
                {
                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "Description", b1 =>
                        {
                            b1.Property<Guid>("PermissionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("Description");

                            b1.HasKey("PermissionId");

                            b1.ToTable("Permissions", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("PermissionId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "Name", b1 =>
                        {
                            b1.Property<Guid>("PermissionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Name");

                            b1.HasKey("PermissionId");

                            b1.ToTable("Permissions", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("PermissionId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "Policy", b1 =>
                        {
                            b1.Property<Guid>("PermissionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Policy");

                            b1.HasKey("PermissionId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("Permissions", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("PermissionId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Policy")
                        .IsRequired();
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.RolePermissions.RolePermission", b =>
                {
                    b.HasOne("Modulith.Commerce.AdminUser.Domain.Permissions.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modulith.Commerce.AdminUser.Domain.Roles.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.Roles.Role", b =>
                {
                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "Description", b1 =>
                        {
                            b1.Property<Guid>("RoleId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("Description");

                            b1.HasKey("RoleId");

                            b1.ToTable("Roles", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("RoleId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "KeycloakRoleName", b1 =>
                        {
                            b1.Property<Guid>("RoleId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("KeycloakRoleName");

                            b1.HasKey("RoleId");

                            b1.ToTable("Roles", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("RoleId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "Name", b1 =>
                        {
                            b1.Property<Guid>("RoleId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Name");

                            b1.HasKey("RoleId");

                            b1.ToTable("Roles", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("RoleId");
                        });

                    b.Navigation("Description");

                    b.Navigation("KeycloakRoleName")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.Teams.Team", b =>
                {
                    b.HasOne("Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser", null)
                        .WithMany("Teams")
                        .HasForeignKey("AdminUserId");

                    b.HasOne("Modulith.Commerce.AdminUser.Domain.Departments.Department", "Department")
                        .WithMany("Teams")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser", "Lead")
                        .WithMany()
                        .HasForeignKey("LeadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "Name", b1 =>
                        {
                            b1.Property<Guid>("TeamId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Name");

                            b1.HasKey("TeamId");

                            b1.ToTable("Teams", "adminusers");

                            b1.WithOwner()
                                .HasForeignKey("TeamId");
                        });

                    b.Navigation("Department");

                    b.Navigation("Lead");

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser", b =>
                {
                    b.Navigation("Departments");

                    b.Navigation("Teams");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.Departments.Department", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.Permissions.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("Modulith.Commerce.AdminUser.Domain.Roles.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
