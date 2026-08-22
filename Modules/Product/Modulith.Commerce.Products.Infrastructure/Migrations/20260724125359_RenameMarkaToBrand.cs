using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modulith.Commerce.Products.Infrastructure.Migrations
{
    public partial class RenameMarkaToBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Markas",
                schema: "products",
                newName: "Brands",
                newSchema: "products");

            migrationBuilder.RenameColumn(
                name: "MarkaId",
                schema: "products",
                table: "Models",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Models_MarkaId",
                schema: "products",
                table: "Models",
                newName: "IX_Models_BrandId");

            migrationBuilder.Sql(
                "EXEC sp_rename N'products.FK_Models_Markas_MarkaId', N'FK_Models_Brands_BrandId', N'OBJECT';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "EXEC sp_rename N'products.FK_Models_Brands_BrandId', N'FK_Models_Markas_MarkaId', N'OBJECT';");

            migrationBuilder.RenameIndex(
                name: "IX_Models_BrandId",
                schema: "products",
                table: "Models",
                newName: "IX_Models_MarkaId");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                schema: "products",
                table: "Models",
                newName: "MarkaId");

            migrationBuilder.RenameTable(
                name: "Brands",
                schema: "products",
                newName: "Markas",
                newSchema: "products");
        }
    }
}
