using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    public class NavigationManager : INavigationManager
    {
        public IDictionary<string, MenuDefinition> Menus { get; private set; }

        public MenuDefinition SideBarMenu
        {
            get
            {
                return Menus["SideBarMenu"];
            }
        }

        public NavigationManager()
        {
            Menus = new Dictionary<string, MenuDefinition>
            {
                { "SideBarMenu", new MenuDefinition("SideBarMenu")}
            };
        }
    }
}
