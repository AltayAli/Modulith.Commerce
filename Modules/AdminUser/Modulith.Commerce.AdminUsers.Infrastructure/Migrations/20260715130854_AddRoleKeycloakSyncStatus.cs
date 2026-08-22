using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modulith.Commerce.AdminUsers.Infrastructure.Migrations
{
    public partial class AddRoleKeycloakSyncStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "adminusers",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "adminusers",
                table: "Roles");
        }
    }
}
