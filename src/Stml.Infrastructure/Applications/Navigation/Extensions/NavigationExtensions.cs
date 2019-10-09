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
            where TNavigationProvider : INavigationProvider<MenuGroup, MenuItem>
        {
            return services.AddNavigationProvider<TNavigationProvider, MenuGroup, MenuItem>();
        }

        public static IServiceCollection AddNavigationProvider<TNavigationProvider, TMenuGroup>(this IServiceCollection services)
            where TNavigationProvider : INavigationProvider<TMenuGroup, MenuItem>
            where TMenuGroup : MenuGroup<MenuItem>
        {
            return services.AddNavigationProvider<TNavigationProvider, TMenuGroup, MenuItem>();
        }

        public static IServiceCollection AddNavigationProvider<TNavigationProvider, TMenuGroup, TMenuItem>(this IServiceCollection services)
            where TNavigationProvider : INavigationProvider<TMenuGroup, TMenuItem>
            where TMenuGroup : MenuGroup<TMenuItem>
            where TMenuItem : MenuItem
        {

            services.AddSingleton(typeof(INavigationManager<TMenuGroup, TMenuItem>), typeof(NavigationManager<TMenuGroup, TMenuItem>));
            services.AddSingleton(typeof(INavigationProviderContext<TMenuGroup, TMenuItem>), typeof(NavigationProviderContext<TMenuGroup, TMenuItem>));
            services.AddSingleton(typeof(INavigationProvider<TMenuGroup, TMenuItem>), typeof(TNavigationProvider));
            return services;
        }

        public static IApplicationBuilder UseNavigationProvider<TNavigationProvider>(this IApplicationBuilder app)
            where TNavigationProvider : INavigationProvider<MenuGroup, MenuItem>
        {
            return app.UseNavigationProvider<TNavigationProvider, MenuGroup, MenuItem>();
        }

        public static IApplicationBuilder UseNavigationProvider<TNavigationProvider, TMenuGroup>(this IApplicationBuilder app)
            where TMenuGroup : MenuGroup<MenuItem>
        {
            return app.UseNavigationProvider<TNavigationProvider, TMenuGroup, MenuItem>();
        }

        public static IApplicationBuilder UseNavigationProvider<TNavigationProvider, TMenuGroup, TMenuItem>(this IApplicationBuilder app)
            where TMenuGroup : MenuGroup<TMenuItem>
            where TMenuItem : MenuItem
        {
            var context
                = app.ApplicationServices.GetService(typeof(INavigationProviderContext<TMenuGroup, TMenuItem>))
                as
                INavigationProviderContext<TMenuGroup, TMenuItem>;
            var provider
                = app.ApplicationServices.GetService(typeof(INavigationProvider<TMenuGroup, TMenuItem>))
                as
                INavigationProvider<TMenuGroup, TMenuItem>;
            provider.SetNavigation(context);
            return app;
        }
    }
}
