using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    public interface INavigationProviderContext<TMenuGroup, TMenuItem>
        where TMenuGroup : MenuGroup<TMenuItem>
        where TMenuItem : MenuItem
    {
        INavigationManager<TMenuGroup, TMenuItem> Manager { get; }
    }
}
