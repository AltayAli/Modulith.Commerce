using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Modulith.Commerce.AdminUser.Domain.AdminUserRoles;
using Modulith.Commerce.AdminUser.Domain.Roles;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Infrastructure.Authorization;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Authorization
{

    public sealed class PermissionClaimsTransformation(IAdminUserRolesRepository adminUserRolesRepository) : IClaimsTransformation
    {
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
                return principal;

            if (identity.HasClaim(c => c.Type == PermissionClaimTypes.Permission))
                return principal;

            string? subject = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? principal.FindFirst("sub")?.Value;

            if (!Guid.TryParse(subject, out var keycloakId))
                return principal;

            var now = DateTime.UtcNow;

            var userRoles = await (await adminUserRolesRepository.SelectAsync(new FilteringOptions<AdminUserRole>
            {
                Predicates =
                [
                    ur => ur.User.KeyCloakId == keycloakId,
                    ur => ur.ExpiredAt == null || ur.ExpiredAt > now,
                    ur => ur.Role.Status == RoleStatus.Active
                ],
                Relations = ["Role.RolePermissions.Permission"]
            })).ToListAsync();

            var policies = userRoles
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission.Policy.Value)
                .Distinct();

            foreach (var policy in policies)
            {
                identity.AddClaim(new Claim(PermissionClaimTypes.Permission, policy));
            }

            return principal;
        }
    }
}
