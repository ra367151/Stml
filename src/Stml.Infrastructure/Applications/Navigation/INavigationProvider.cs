using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    public interface INavigationProvider<TMenuDefinition, TMenuItemDefinition>
        where TMenuDefinition : MenuDefinition<TMenuItemDefinition>
        where TMenuItemDefinition : MenuItemDefinition
    {
        void SetNavigation(INavigationProviderContext<TMenuDefinition, TMenuItemDefinition> context);
    }
}
