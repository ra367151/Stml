using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Authorizations.Permissions
{
    public interface IPermissionProviderContext<TPermission> where TPermission : Permission
    {
        IPermissionManager<TPermission> Manager { get; }
    }
}
