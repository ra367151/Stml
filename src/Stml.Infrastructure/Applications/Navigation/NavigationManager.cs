using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    public class NavigationManager<TMenuDefinition, TMenuItemDefinition> : INavigationManager<TMenuDefinition, TMenuItemDefinition>
        where TMenuDefinition : MenuDefinition<TMenuItemDefinition>
        where TMenuItemDefinition : MenuItemDefinition
    {
        public Guid Id { get; private set; }
        public IDictionary<string, TMenuDefinition> Menus { get; private set; }

        public TMenuDefinition SidebarMenu
        {
            get
            {
                return Menus["SidebarMenu"];
            }
        }

        public NavigationManager()
        {
            Id = Guid.NewGuid();
            Menus = new Dictionary<string, TMenuDefinition>
            {
                { "SidebarMenu", (TMenuDefinition)Activator.CreateInstance(typeof(TMenuDefinition), "SidebarMenu")}
            };
        }
    }

}
