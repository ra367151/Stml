using Stml.Infrastructure.Applications.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stml.Web.Startup
{
    public class StmlNavigationProvider : INavigationProvider<MenuGroup, MenuItem>
    {
        public void SetNavigation(INavigationProviderContext<MenuGroup, MenuItem> context)
        {
            context.Manager.MainMenu.AddItem(
                new MenuItem(
                    PageNames.Home,
                    "Home",
                    "cui-home"
                )
            ).AddItem(
                new MenuItem(
                    PageNames.User,
                    "Users",
                    "cui-people"
                )
            ).AddItem(
                new MenuItem(
                    PageNames.Role,
                    "Roles",
                    "cui-tags"
                )
            ).AddItem(
                new MenuItem(
                    "产品",
                    "Products",
                    "icon-list"
                )
            ).AddItem(
                new MenuItem(
                    PageNames.Setting,
                    "Settings",
                    "cui-settings"
                )
            );
        }
    }
}
