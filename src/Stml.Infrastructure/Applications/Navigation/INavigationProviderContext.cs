using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    public interface INavigationProviderContext<TMenuDefinition, TMenuItemDefinition>
        where TMenuDefinition : MenuDefinition<TMenuItemDefinition>
        where TMenuItemDefinition : MenuItemDefinition
    {
        INavigationManager<TMenuDefinition, TMenuItemDefinition> Manager { get; }
    }
}
