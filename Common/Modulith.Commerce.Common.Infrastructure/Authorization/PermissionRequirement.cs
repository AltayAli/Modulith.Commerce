using Microsoft.AspNetCore.Authorization;

namespace Modulith.Commerce.Common.Infrastructure.Authorization
{
    public sealed class PermissionRequirement(string policy) : IAuthorizationRequirement
    {
        public string Policy { get; } = policy;
    }
}
