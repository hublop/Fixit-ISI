using System.Security.Claims;
using Fixit.WebApi.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Fixit.WebApi.Extensions
{
    internal static class AuthWithRolesExtension
    {
        internal static void AddAuthorizationWithRoles(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(RolePolicies.RequireAuthorizedUser, policy =>
                {
                    policy.AuthenticationSchemes.Add("Bearer");
                    policy.RequireClaim(ClaimTypes.NameIdentifier);
                });
                options.AddPolicy(RolePolicies.RequireAdmin, policy =>
                {
                    policy.AuthenticationSchemes.Add("Bearer");
                    policy.RequireClaim(ClaimTypes.NameIdentifier);
                    policy.RequireRole(Roles.Admin);
                });
                options.AddPolicy(RolePolicies.RequireCustomer, policy =>
                {
                    policy.AuthenticationSchemes.Add("Bearer");
                    policy.RequireClaim(ClaimTypes.NameIdentifier);
                    policy.RequireRole(Roles.Admin, Roles.Customer);
                });
                options.AddPolicy(RolePolicies.RequireContractor, policy =>
                {
                    policy.AuthenticationSchemes.Add("Bearer");
                    policy.RequireClaim(ClaimTypes.NameIdentifier);
                    policy.RequireRole(Roles.Admin, Roles.Contractor);
                });
            });
        }
    }
}