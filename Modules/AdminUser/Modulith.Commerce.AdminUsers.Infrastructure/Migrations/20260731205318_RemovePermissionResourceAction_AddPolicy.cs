using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modulith.Commerce.AdminUsers.Infrastructure.Migrations
{
    public partial class RemovePermissionResourceAction_AddPolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                schema: "adminusers",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Resource",
                schema: "adminusers",
                table: "Permissions");

            migrationBuilder.AddColumn<string>(
                name: "Policy",
                schema: "adminusers",
                table: "Permissions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Policy",
                schema: "adminusers",
                table: "Permissions",
                column: "Policy",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_Policy",
                schema: "adminusers",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Policy",
                schema: "adminusers",
                table: "Permissions");

            migrationBuilder.AddColumn<string>(
                name: "Action",
                schema: "adminusers",
                table: "Permissions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Resource",
                schema: "adminusers",
                table: "Permissions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
