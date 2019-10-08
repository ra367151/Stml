using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    public abstract class NavigationProvider
    {
        public abstract void SetNavigation(INavigationProviderContext context);
    }
}
