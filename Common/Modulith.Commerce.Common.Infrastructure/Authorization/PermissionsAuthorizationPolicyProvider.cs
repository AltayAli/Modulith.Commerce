using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Modulith.Commerce.Common.Infrastructure.Authorization
{

    public sealed class PermissionsAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _authorizationOptions;
        private readonly DefaultAuthorizationPolicyProvider _fallbackPolicyProvider;

        public PermissionsAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            _authorizationOptions = options.Value;
            _fallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => _fallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => _fallbackPolicyProvider.GetFallbackPolicyAsync();

        public async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var existedPolicy = await _fallbackPolicyProvider.GetPolicyAsync(policyName);

            if (existedPolicy != null)
                return existedPolicy;

            var policy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddRequirements(new PermissionRequirement(policyName))
                .Build();

            _authorizationOptions.AddPolicy(policyName, policy);

            return policy;
        }
    }
}
