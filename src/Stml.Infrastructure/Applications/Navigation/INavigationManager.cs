using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    public interface INavigationManager
    {
        IDictionary<string, MenuDefinition> Menus { get; }
        MenuDefinition SideBarMenu { get; }
    }
}
