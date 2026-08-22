using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modulith.Commerce.AdminUsers.Infrastructure.Migrations
{

    public partial class SeedAdminUserRolePermissions : Migration
    {
        private static readonly Guid AdministratorRoleId = new("a069c55c-6af7-4f47-b99c-a1816e5c40c2");

        private static readonly Guid AdminUserRoleReadId = new("1fefb8d2-121c-46d0-99ea-a414fa9d5a9f");
        private static readonly Guid AdminUserRoleWriteId = new("9992dd7a-8120-4dfe-afff-d9e11c48d552");
        private static readonly Guid AdminUserRoleDeleteId = new("ad243217-1c46-47ea-a625-0b6f7066dc36");

        private static readonly Guid AdminUserRoleReadRolePermissionId = new("383437c5-ea6f-41bb-b72c-eec01de016e6");
        private static readonly Guid AdminUserRoleWriteRolePermissionId = new("fb61e518-bf6f-4bd8-a4b3-e447c9d22039");
        private static readonly Guid AdminUserRoleDeleteRolePermissionId = new("7e0ddc1e-0171-4340-8a68-f2dcf3dc5ce2");

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "adminusers",
                table: "Permissions",
                columns: new[] { "Id", "Name", "Description", "Policy" },
                values: new object[,]
                {
                    { AdminUserRoleReadId, "AdminUserRoleRead", "Kullaniciya atanmis rolleri goruntuleme", "adminuserrole:read" },
                    { AdminUserRoleWriteId, "AdminUserRoleWrite", "Kullaniciya rol atama", "adminuserrole:write" },
                    { AdminUserRoleDeleteId, "AdminUserRoleDelete", "Kullanicidan rol kaldirma", "adminuserrole:delete" }
                });

            migrationBuilder.InsertData(
                schema: "adminusers",
                table: "RolePermissions",
                columns: new[] { "Id", "RoleId", "PermissionId" },
                values: new object[,]
                {
                    { AdminUserRoleReadRolePermissionId, AdministratorRoleId, AdminUserRoleReadId },
                    { AdminUserRoleWriteRolePermissionId, AdministratorRoleId, AdminUserRoleWriteId },
                    { AdminUserRoleDeleteRolePermissionId, AdministratorRoleId, AdminUserRoleDeleteId }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "adminusers",
                table: "RolePermissions",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    AdminUserRoleReadRolePermissionId, AdminUserRoleWriteRolePermissionId, AdminUserRoleDeleteRolePermissionId
                });

            migrationBuilder.DeleteData(
                schema: "adminusers",
                table: "Permissions",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    AdminUserRoleReadId, AdminUserRoleWriteId, AdminUserRoleDeleteId
                });
        }
    }
}
