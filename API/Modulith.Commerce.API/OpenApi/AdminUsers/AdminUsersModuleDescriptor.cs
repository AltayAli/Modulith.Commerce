using Asp.Versioning;

namespace Modulith.Commerce.API.OpenApi.AdminUsers;

public sealed class AdminUsersModuleDescriptor : IModuleDescriptor
{
    public string Key => "admin-users";
    public string Title => "Admin Users API";
    public IReadOnlyList<ApiVersion> Versions { get; } = [new ApiVersion(1, 0)];
    public string? Description => "Manages admin users, roles, and permissions.";
}
