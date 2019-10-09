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
        public IDictionary<string, TMenuGroup> MenuGroups { get; private set; }

        public TMenuGroup MainMenu
        {
            get
            {
                return MenuGroups["MainMenu"];
            }
        }

        public NavigationManager()
        {
            Id = Guid.NewGuid();
            MenuGroups = new Dictionary<string, TMenuGroup>
            {
                { "MainMenu", (TMenuGroup)Activator.CreateInstance(typeof(TMenuGroup), "MainMenu")}
            };
        }
    }

}
