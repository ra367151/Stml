using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation.Extensions
{
    public static class NavigationExtensions
    {
        public static IServiceCollection AddNavigation<TNavigationProvider>(this IServiceCollection services)
            where TNavigationProvider : NavigationProvider
        {
            services.AddSingleton<INavigationManager, NavigationManager>();
            services.AddSingleton<INavigationProviderContext, NavigationProviderContext>();
            services.AddSingleton(typeof(NavigationProvider), typeof(TNavigationProvider));
            return services;
        }

        public static IApplicationBuilder UseNavigationProvider<TNavigationProvider>(this IApplicationBuilder app)
            where TNavigationProvider : NavigationProvider
        {
            var context = app.ApplicationServices.GetService<INavigationProviderContext>();
            var provider = app.ApplicationServices.GetService<NavigationProvider>();
            provider.SetNavigation(context);
            return app;
        }
    }
}
