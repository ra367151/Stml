using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation.Extensions
{
    public static class NavigationExtensions
    {
        public static IServiceCollection AddNavigationProvider<TNavigationProvider>(this IServiceCollection services)
            where TNavigationProvider : INavigationProvider<MenuDefinition, MenuItemDefinition>
        {
            return services.AddNavigationProvider<TNavigationProvider, MenuDefinition, MenuItemDefinition>();
        }

        public static IServiceCollection AddNavigationProvider<TNavigationProvider, TMenuDefinition>(this IServiceCollection services)
            where TNavigationProvider : INavigationProvider<TMenuDefinition, MenuItemDefinition>
            where TMenuDefinition : MenuDefinition<MenuItemDefinition>
        {
            return services.AddNavigationProvider<TNavigationProvider, TMenuDefinition, MenuItemDefinition>();
        }

        public static IServiceCollection AddNavigationProvider<TNavigationProvider, TMenuDefinition, TMenuItemDefinition>(this IServiceCollection services)
            where TNavigationProvider : INavigationProvider<TMenuDefinition, TMenuItemDefinition>
            where TMenuDefinition : MenuDefinition<TMenuItemDefinition>
            where TMenuItemDefinition : MenuItemDefinition
        {

            services.AddSingleton(typeof(INavigationManager<TMenuDefinition, TMenuItemDefinition>), typeof(NavigationManager<TMenuDefinition, TMenuItemDefinition>));
            services.AddSingleton(typeof(INavigationProviderContext<TMenuDefinition, TMenuItemDefinition>), typeof(NavigationProviderContext<TMenuDefinition, TMenuItemDefinition>));
            services.AddSingleton(typeof(INavigationProvider<TMenuDefinition, TMenuItemDefinition>), typeof(TNavigationProvider));
            return services;
        }

        public static IApplicationBuilder UseNavigationProvider<TNavigationProvider>(this IApplicationBuilder app)
            where TNavigationProvider : INavigationProvider<MenuDefinition, MenuItemDefinition>
        {
            return app.UseNavigationProvider<TNavigationProvider, MenuDefinition, MenuItemDefinition>();
        }

        public static IApplicationBuilder UseNavigationProvider<TNavigationProvider, TMenuDefinition>(this IApplicationBuilder app)
            where TMenuDefinition : MenuDefinition<MenuItemDefinition>
        {
            return app.UseNavigationProvider<TNavigationProvider, TMenuDefinition, MenuItemDefinition>();
        }

        public static IApplicationBuilder UseNavigationProvider<TNavigationProvider, TMenuDefinition, TMenuItemDefinition>(this IApplicationBuilder app)
            where TMenuDefinition : MenuDefinition<TMenuItemDefinition>
            where TMenuItemDefinition : MenuItemDefinition
        {
            var context
                = app.ApplicationServices.GetService(typeof(INavigationProviderContext<TMenuDefinition, TMenuItemDefinition>))
                as
                INavigationProviderContext<TMenuDefinition, TMenuItemDefinition>;
            var provider
                = app.ApplicationServices.GetService(typeof(INavigationProvider<TMenuDefinition, TMenuItemDefinition>))
                as
                INavigationProvider<TMenuDefinition, TMenuItemDefinition>;
            provider.SetNavigation(context);
            return app;
        }
    }
}
