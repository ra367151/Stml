using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.Authorizations.Permissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Authorizations.Extensions
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection ConfigureAuthorization<TUserClaimsPrincipalFactory, TUser, TRole>(this IServiceCollection services)
            where TUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<TUser, TRole>
            where TUser : class
            where TRole : class
        {
            services.AddScoped<IUserClaimsPrincipalFactory<TUser>, TUserClaimsPrincipalFactory>();
            services.AddSingleton<IPermissionPacker, PermissionPacker>();
            services.AddSingleton<IPermissionChecker, PermissionChecker>();
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            return services;
        }
    }
}
