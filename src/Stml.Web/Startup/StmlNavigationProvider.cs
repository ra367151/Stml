using Stml.Infrastructure.Applications.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stml.Web.Startup
{
    public class StmlNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.SideBarMenu.AddItem(
                new MenuItemDefinition(
                    PageNames.Home,
                    "Home",
                    "icon-home"
                )
            ).AddItem(
                new MenuItemDefinition(
                    PageNames.User,
                    "Users",
                    "icon-user"
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
                    "icon-settings"
                )
            );
        }
    }
}
