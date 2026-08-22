using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modulith.Commerce.AdminUsers.Infrastructure.Migrations
{

    public partial class SeedBootstrapAdminRoleAndPermissions : Migration
    {
        private static readonly Guid AdministratorRoleId = new("a069c55c-6af7-4f47-b99c-a1816e5c40c2");
        private static readonly Guid MemberRoleId = new("af25487f-ae30-4c24-b3d9-9bbe51e0a891");

        private static readonly Guid CategoryReadId = new("5e1874ed-28e1-4fd7-a0cd-c728bff4736e");
        private static readonly Guid CategoryWriteId = new("90b96638-089d-46ca-bd86-240633e8752f");
        private static readonly Guid CategoryDeleteId = new("4e0ef30d-41e1-44f0-8fc5-e1469cb308dc");
        private static readonly Guid CategoryPropertyReadId = new("8cf2feca-cd67-4e2c-858a-3429188776d2");
        private static readonly Guid CategoryPropertyWriteId = new("59e36903-803b-49e1-aebb-ae3d247e90d9");
        private static readonly Guid ProductVariantWriteId = new("1b7e1582-861b-4eeb-9589-6144a9a9da75");
        private static readonly Guid ProductVariantDeleteId = new("e39e1f67-2d46-413d-b6d9-a4790c49b8b8");
        private static readonly Guid BrandReadId = new("9a0ca59a-4417-419a-8052-e4f7204e67f4");
        private static readonly Guid BrandWriteId = new("04757c72-e666-4e10-a1da-b5c81dbf65ae");
        private static readonly Guid BrandDeleteId = new("0edfbabe-e758-455c-b7b9-51c9da0b9174");
        private static readonly Guid ModelReadId = new("c21b9f2a-2637-4472-92d2-a20d4e8255f4");
        private static readonly Guid ModelWriteId = new("44670624-d21e-4f3b-8e8f-2741bd7a9565");
        private static readonly Guid ModelDeleteId = new("ab36acf6-996d-48f5-b3bc-d8c0872753fc");
        private static readonly Guid ProductReadId = new("7cf1bae1-e234-4463-8cbf-74ce344d5078");
        private static readonly Guid ProductWriteId = new("f35b28ca-70cd-4062-855e-7e2a3acf72a0");
        private static readonly Guid ProductDeleteId = new("e971dfa0-9808-469c-a08a-cf5201476117");
        private static readonly Guid AdminUserReadId = new("ff316824-b396-4647-8b92-ab81b8369df6");
        private static readonly Guid AdminUserWriteId = new("927034ef-7b4d-4ed9-8a3a-0c01e33d892f");
        private static readonly Guid AdminUserDeleteId = new("c338279a-8827-487e-8c24-5868d5d84931");
        private static readonly Guid DepartmentReadId = new("e1229953-d2f8-45a1-89c9-e21b88ca08c0");
        private static readonly Guid DepartmentWriteId = new("4cfb94e3-1432-4258-8c98-39ec02c41e8e");
        private static readonly Guid DepartmentDeleteId = new("231175f8-de60-4fd6-9e10-c44cc9f4c34b");
        private static readonly Guid RoleReadId = new("4462cc97-6506-4529-b267-0bacf33f0976");
        private static readonly Guid RoleWriteId = new("ab31e4c7-4bca-4cbe-acc8-e7913274ae32");
        private static readonly Guid RoleDeleteId = new("c8dcf243-e216-49b7-9aba-d3d463b56018");
        private static readonly Guid RolePermissionReadId = new("fe21dfcf-fdd1-4099-98ab-6c0bca35ecb4");
        private static readonly Guid RolePermissionWriteId = new("5367b93e-fc98-4051-b56f-f29f1264f6d0");
        private static readonly Guid RolePermissionDeleteId = new("c416f646-e02f-4e24-a9c3-dd3e4ff39f77");
        private static readonly Guid TeamWriteId = new("748408ef-98ab-40ee-a7ec-9ed689dc0247");
        private static readonly Guid TeamDeleteId = new("5e0479dd-b9a1-49f9-8c25-805777f336b5");

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "adminusers",
                table: "Roles",
                columns: new[] { "Id", "Name", "Description", "IsSystemRole", "KeycloakRoleName", "Status" },
                values: new object[,]
                {
                    { AdministratorRoleId, "Administrator", "Sistemin tum yetkilerine sahip, silinemez bootstrap rolu.", true, "administrator", 0 },
                    { MemberRoleId, "Member", "Varsayilan yetkisiz uye rolu - yetkiler sonradan tek tek atanir.", false, "member", 0 }
                });

            migrationBuilder.InsertData(
                schema: "adminusers",
                table: "Permissions",
                columns: new[] { "Id", "Name", "Description", "Policy" },
                values: new object[,]
                {
                    { CategoryReadId, "CategoryRead", "Kategori listeleme/goruntuleme", "category:read" },
                    { CategoryWriteId, "CategoryWrite", "Kategori olusturma/guncelleme", "category:write" },
                    { CategoryDeleteId, "CategoryDelete", "Kategori silme", "category:delete" },
                    { CategoryPropertyReadId, "CategoryPropertyRead", "Kategori ozelligi goruntuleme", "categoryproperty:read" },
                    { CategoryPropertyWriteId, "CategoryPropertyWrite", "Kategori ozelligi olusturma/guncelleme", "categoryproperty:write" },
                    { ProductVariantWriteId, "ProductVariantWrite", "Urun varyanti olusturma/guncelleme", "productvariant:write" },
                    { ProductVariantDeleteId, "ProductVariantDelete", "Urun varyanti silme", "productvariant:delete" },
                    { BrandReadId, "BrandRead", "Marka goruntuleme", "brand:read" },
                    { BrandWriteId, "BrandWrite", "Marka olusturma/guncelleme", "brand:write" },
                    { BrandDeleteId, "BrandDelete", "Marka silme", "brand:delete" },
                    { ModelReadId, "ModelRead", "Model goruntuleme", "model:read" },
                    { ModelWriteId, "ModelWrite", "Model olusturma/guncelleme", "model:write" },
                    { ModelDeleteId, "ModelDelete", "Model silme", "model:delete" },
                    { ProductReadId, "ProductRead", "Urun goruntuleme", "product:read" },
                    { ProductWriteId, "ProductWrite", "Urun olusturma/guncelleme", "product:write" },
                    { ProductDeleteId, "ProductDelete", "Urun silme", "product:delete" },
                    { AdminUserReadId, "AdminUserRead", "Admin kullanici goruntuleme", "adminuser:read" },
                    { AdminUserWriteId, "AdminUserWrite", "Admin kullanici olusturma/guncelleme", "adminuser:write" },
                    { AdminUserDeleteId, "AdminUserDelete", "Admin kullanici silme", "adminuser:delete" },
                    { DepartmentReadId, "DepartmentRead", "Departman goruntuleme", "department:read" },
                    { DepartmentWriteId, "DepartmentWrite", "Departman olusturma/guncelleme", "department:write" },
                    { DepartmentDeleteId, "DepartmentDelete", "Departman silme", "department:delete" },
                    { RoleReadId, "RoleRead", "Rol goruntuleme", "role:read" },
                    { RoleWriteId, "RoleWrite", "Rol olusturma/guncelleme", "role:write" },
                    { RoleDeleteId, "RoleDelete", "Rol silme", "role:delete" },
                    { RolePermissionReadId, "RolePermissionRead", "Role atanmis permission'lari goruntuleme", "rolepermission:read" },
                    { RolePermissionWriteId, "RolePermissionWrite", "Role permission atama", "rolepermission:write" },
                    { RolePermissionDeleteId, "RolePermissionDelete", "Rolden permission kaldirma", "rolepermission:delete" },
                    { TeamWriteId, "TeamWrite", "Takim olusturma/guncelleme/uye yonetimi", "team:write" },
                    { TeamDeleteId, "TeamDelete", "Takim silme", "team:delete" }
                });

            var administratorPermissionIds = new[]
            {
                CategoryReadId, CategoryWriteId, CategoryDeleteId,
                CategoryPropertyReadId, CategoryPropertyWriteId,
                ProductVariantWriteId, ProductVariantDeleteId,
                BrandReadId, BrandWriteId, BrandDeleteId,
                ModelReadId, ModelWriteId, ModelDeleteId,
                ProductReadId, ProductWriteId, ProductDeleteId,
                AdminUserReadId, AdminUserWriteId, AdminUserDeleteId,
                DepartmentReadId, DepartmentWriteId, DepartmentDeleteId,
                RoleReadId, RoleWriteId, RoleDeleteId,
                RolePermissionReadId, RolePermissionWriteId, RolePermissionDeleteId,
                TeamWriteId, TeamDeleteId
            };

            var rolePermissionRows = new object[administratorPermissionIds.Length, 3];
            for (int i = 0; i < administratorPermissionIds.Length; i++)
            {
                rolePermissionRows[i, 0] = Guid.NewGuid();
                rolePermissionRows[i, 1] = AdministratorRoleId;
                rolePermissionRows[i, 2] = administratorPermissionIds[i];
            }

            migrationBuilder.InsertData(
                schema: "adminusers",
                table: "RolePermissions",
                columns: new[] { "Id", "RoleId", "PermissionId" },
                values: rolePermissionRows);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "DELETE FROM [adminusers].[RolePermissions] WHERE [RoleId] = 'a069c55c-6af7-4f47-b99c-a1816e5c40c2';");

            migrationBuilder.DeleteData(
                schema: "adminusers",
                table: "Permissions",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    CategoryReadId, CategoryWriteId, CategoryDeleteId,
                    CategoryPropertyReadId, CategoryPropertyWriteId,
                    ProductVariantWriteId, ProductVariantDeleteId,
                    BrandReadId, BrandWriteId, BrandDeleteId,
                    ModelReadId, ModelWriteId, ModelDeleteId,
                    ProductReadId, ProductWriteId, ProductDeleteId,
                    AdminUserReadId, AdminUserWriteId, AdminUserDeleteId,
                    DepartmentReadId, DepartmentWriteId, DepartmentDeleteId,
                    RoleReadId, RoleWriteId, RoleDeleteId,
                    RolePermissionReadId, RolePermissionWriteId, RolePermissionDeleteId,
                    TeamWriteId, TeamDeleteId
                });

            migrationBuilder.DeleteData(
                schema: "adminusers",
                table: "Roles",
                keyColumn: "Id",
                keyValues: new object[] { AdministratorRoleId, MemberRoleId });
        }
    }
}
