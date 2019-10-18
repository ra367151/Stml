using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Authorizations.Permissions.Extensions
{
    public static class PermissionServiceExtensions
    {
        public static IServiceCollection AddPermissionProvider<TProvider, TPermission>(this IServiceCollection services)
            where TProvider : IPermissionProvider<TPermission>
            where TPermission : Permission
        {
            services.AddSingleton(typeof(IPermissionProvider<TPermission>), typeof(TProvider));
            services.AddSingleton(typeof(IPermissionManager<TPermission>), typeof(PermissionManager<TPermission>));
            services.AddSingleton(typeof(IPermissionProviderContext<TPermission>), typeof(PermissionProviderContext<TPermission>));
            return services;
        }

        public static IServiceCollection AddPermissionProvider<TProvider>(this IServiceCollection services)
            where TProvider : IPermissionProvider<Permission>
        {
            return services.AddPermissionProvider<TProvider, Permission>();
        }

        public static IApplicationBuilder UsePermissionProvider<TProvider, TPermission>(this IApplicationBuilder app)
            where TProvider : IPermissionProvider<TPermission>
            where TPermission : Permission
        {
            var context =
                app.ApplicationServices.GetService(typeof(IPermissionProviderContext<TPermission>))
                as
                IPermissionProviderContext<TPermission>;
            var provider =
                app.ApplicationServices.GetService(typeof(IPermissionProvider<TPermission>))
                as
                IPermissionProvider<TPermission>;
            provider.Initialize(context);
            return app;
        }

        public static IApplicationBuilder UsePermissionProvider<TProvider>(this IApplicationBuilder app)
            where TProvider : IPermissionProvider<Permission>
        {
            return app.UsePermissionProvider<TProvider, Permission>();
        }
    }
}
