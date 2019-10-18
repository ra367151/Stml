using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Authorizations.Permissions
{
    public interface IPermissionProvider<TPermission> where TPermission : Permission
    {
        void Initialize(IPermissionProviderContext<TPermission> context);
    }
}
