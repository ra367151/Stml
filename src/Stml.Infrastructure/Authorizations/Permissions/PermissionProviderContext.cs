using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Authorizations.Permissions
{
    public class PermissionProviderContext<TPermission> : IPermissionProviderContext<TPermission> where TPermission : Permission
    {
        public IPermissionManager<TPermission> Manager { get; }

        public PermissionProviderContext(IPermissionManager<TPermission> manager)
        {
            Manager = manager;
        }
    }
}
