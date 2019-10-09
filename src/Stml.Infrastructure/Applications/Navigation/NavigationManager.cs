using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    public class NavigationManager<TMenuGroup, TMenuItem> : INavigationManager<TMenuGroup, TMenuItem>
        where TMenuGroup : MenuGroup<TMenuItem>
        where TMenuItem : MenuItem
    {
        public Guid Id { get; private set; }
        public IDictionary<string, TMenuGroup> Menus { get; private set; }

        public TMenuGroup SidebarMenu
        {
            get
            {
                return Menus["SidebarMenu"];
            }
        }

        public NavigationManager()
        {
            Id = Guid.NewGuid();
            Menus = new Dictionary<string, TMenuGroup>
            {
                { "SidebarMenu", (TMenuGroup)Activator.CreateInstance(typeof(TMenuGroup), "SidebarMenu")}
            };
        }
    }

}
