using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    public class NavigationProviderContext<TMenuGroup, TMenuItem> : INavigationProviderContext<TMenuGroup, TMenuItem>
        where TMenuGroup : MenuGroup<TMenuItem>
        where TMenuItem : MenuItem
    {
        public INavigationManager<TMenuGroup, TMenuItem> Manager { get; private set; }

        public NavigationProviderContext(INavigationManager<TMenuGroup, TMenuItem> manager)
        {
            Manager = manager;
        }
    }
}
