using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    public interface INavigationManager<TMenuGroup, TMenuItem>
        where TMenuGroup : MenuGroup<TMenuItem>
        where TMenuItem : MenuItem
    {
        Guid Id { get; }

        IDictionary<string, TMenuGroup> MenuGroups { get; }
        TMenuGroup MainMenu { get; }
    }
}
