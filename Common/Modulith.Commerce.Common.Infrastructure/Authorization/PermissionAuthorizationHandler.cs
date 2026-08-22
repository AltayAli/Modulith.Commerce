using Microsoft.AspNetCore.Authorization;

namespace Modulith.Commerce.Common.Infrastructure.Authorization
{
    public sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.HasClaim(PermissionClaimTypes.Permission, requirement.Policy))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
