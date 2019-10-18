﻿using Stml.Infrastructure.Applications.Navigation;
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
                        "cui-home",
                        PermissionNames.VistHome
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
