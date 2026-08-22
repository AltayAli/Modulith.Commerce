using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modulith.Commerce.Products.Infrastructure.Migrations
{
    public partial class AddProductSeoAndStatusFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "products",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);

            migrationBuilder.AddColumn<decimal>(
                name: "AvgRating",
                schema: "products",
                table: "Products",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                schema: "products",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PublishedAt",
                schema: "products",
                table: "Products",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewCount",
                schema: "products",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Seo",
                schema: "products",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                schema: "products",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "products",
                table: "Products",
                type: "nvarchar(280)",
                maxLength: 280,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "products",
                table: "Products",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxClass",
                schema: "products",
                table: "Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "standard");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Slug",
                schema: "products",
                table: "Products",
                column: "Slug",
                unique: true,
                filter: "[DeletedDate] IS NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Slug",
                schema: "products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AvgRating",
                schema: "products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                schema: "products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PublishedAt",
                schema: "products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ReviewCount",
                schema: "products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Seo",
                schema: "products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                schema: "products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TaxClass",
                schema: "products",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "products",
                table: "Products",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
