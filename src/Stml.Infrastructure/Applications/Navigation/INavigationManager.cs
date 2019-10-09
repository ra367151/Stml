﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    public interface INavigationManager<TMenuDefinition, TMenuItemDefinition>
        where TMenuDefinition : MenuDefinition<TMenuItemDefinition>
        where TMenuItemDefinition : MenuItemDefinition
    {
        Guid Id { get; }
        IDictionary<string, TMenuDefinition> Menus { get; }
        TMenuDefinition SidebarMenu { get; }
    }
}
