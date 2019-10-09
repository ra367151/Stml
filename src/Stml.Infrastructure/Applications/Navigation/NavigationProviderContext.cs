using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    public class NavigationProviderContext<TMenuDefinition, TMenuItemDefinition> : INavigationProviderContext<TMenuDefinition, TMenuItemDefinition>
        where TMenuDefinition : MenuDefinition<TMenuItemDefinition>
        where TMenuItemDefinition : MenuItemDefinition
    {
        public INavigationManager<TMenuDefinition, TMenuItemDefinition> Manager { get; private set; }

        public NavigationProviderContext(INavigationManager<TMenuDefinition, TMenuItemDefinition> manager)
        {
            Manager = manager;
        }
    }
}
