using Stml.Infrastructure.Applications.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stml.Web.Startup
{
    public class StmlNavigationProvider : INavigationProvider<MenuDefinition, MenuItemDefinition>
    {
        public void SetNavigation(INavigationProviderContext<MenuDefinition, MenuItemDefinition> context)
        {
            context.Manager.SidebarMenu.AddItem(
                new MenuItemDefinition(
                    PageNames.Home,
                    "Home",
                    "cui-home"
                )
            ).AddItem(
                new MenuItemDefinition(
                    PageNames.User,
                    "Users",
                    "cui-people"
                )
            ).AddItem(
                new MenuItemDefinition(
                    PageNames.Role,
                    "Roles",
                    "cui-tags"
                )
            ).AddItem(
                new MenuItemDefinition(
                    "产品",
                    "Products",
                    "icon-list"
                )
            ).AddItem(
                new MenuItemDefinition(
                    PageNames.Setting,
                    "Settings",
                    "cui-settings"
                )
            );
        }
    }
}
