using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modulith.Commerce.Products.Infrastructure.Migrations
{
    public partial class AddProductsSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "products");

            migrationBuilder.RenameTable(
                name: "ProductVariants",
                newName: "ProductVariants",
                newSchema: "products");

            migrationBuilder.RenameTable(
                name: "ProductVariantProperties",
                newName: "ProductVariantProperties",
                newSchema: "products");

            migrationBuilder.RenameTable(
                name: "ProductVariantImages",
                newName: "ProductVariantImages",
                newSchema: "products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Products",
                newSchema: "products");

            migrationBuilder.RenameTable(
                name: "ProductCategories",
                newName: "ProductCategories",
                newSchema: "products");

            migrationBuilder.RenameTable(
                name: "OutboxMessages",
                newName: "OutboxMessages",
                newSchema: "products");

            migrationBuilder.RenameTable(
                name: "Models",
                newName: "Models",
                newSchema: "products");

            migrationBuilder.RenameTable(
                name: "Markas",
                newName: "Markas",
                newSchema: "products");

            migrationBuilder.RenameTable(
                name: "CategoryPropertyValues",
                newName: "CategoryPropertyValues",
                newSchema: "products");

            migrationBuilder.RenameTable(
                name: "CategoryProperties",
                newName: "CategoryProperties",
                newSchema: "products");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categories",
                newSchema: "products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ProductVariants",
                schema: "products",
                newName: "ProductVariants");

            migrationBuilder.RenameTable(
                name: "ProductVariantProperties",
                schema: "products",
                newName: "ProductVariantProperties");

            migrationBuilder.RenameTable(
                name: "ProductVariantImages",
                schema: "products",
                newName: "ProductVariantImages");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "products",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "ProductCategories",
                schema: "products",
                newName: "ProductCategories");

            migrationBuilder.RenameTable(
                name: "OutboxMessages",
                schema: "products",
                newName: "OutboxMessages");

            migrationBuilder.RenameTable(
                name: "Models",
                schema: "products",
                newName: "Models");

            migrationBuilder.RenameTable(
                name: "Markas",
                schema: "products",
                newName: "Markas");

            migrationBuilder.RenameTable(
                name: "CategoryPropertyValues",
                schema: "products",
                newName: "CategoryPropertyValues");

            migrationBuilder.RenameTable(
                name: "CategoryProperties",
                schema: "products",
                newName: "CategoryProperties");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "products",
                newName: "Categories");
        }
    }
}
