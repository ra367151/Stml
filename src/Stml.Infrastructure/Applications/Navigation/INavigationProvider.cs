using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    public interface INavigationProvider<TMenuGroup, TMenuItem>
        where TMenuGroup : MenuGroup<TMenuItem>
        where TMenuItem : MenuItem
    {
        void Initialize(INavigationProviderContext<TMenuGroup, TMenuItem> context);
    }
}
