namespace Modulith.Commerce.API.OpenApi;

public static class ModuleRegistry
{
    public static IReadOnlyList<IModuleDescriptor> BuildModules() =>
    [
        new Products.ProductsModuleDescriptor(),
        new AdminUsers.AdminUsersModuleDescriptor(),
    ];
}
