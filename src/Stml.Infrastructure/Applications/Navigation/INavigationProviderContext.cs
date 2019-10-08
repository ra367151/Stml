using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Navigation
{
    public interface INavigationProviderContext
    {
        INavigationManager Manager { get; }
    }
}
