using Stml.Infrastructure.Applications.Navigation;
using Stml.Web.Startup.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stml.Web.Startup.Navigations
{
    public class StmlNavigationProvider : INavigationProvider<MenuGroup, MenuItem>
    {
        public void Initialize(INavigationProviderContext<MenuGroup, MenuItem> context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItem(
                        PageNames.Home,
                        "Home",
                        "cui-home"
                    )
                ).AddItem(
                    new MenuItem(
                        PageNames.Role,
                        "Role",
                        "cui-tags",
                        PermissionNames.VisitRolePage
                    )
                ).AddItem(
                    new MenuItem(
                        PageNames.User,
                        "User",
                        "cui-people",
                        PermissionNames.VisitUserPage
                    )
                );
        }
    }
}
