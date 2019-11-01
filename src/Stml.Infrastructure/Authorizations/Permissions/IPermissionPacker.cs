using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Authorizations.Permissions
{
    public interface IPermissionPacker
    {
        string PackPermissionsIntoString<TPermission>(IEnumerable<TPermission> permissions) where TPermission : Permission;
        IEnumerable<TPermission> UnPackPermissionFromString<TPermission>(string packedPermissions) where TPermission : Permission;
    }
}
