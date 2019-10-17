using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Authorizations.Permissions
{
    public interface IPermissionPacker
    {
        string PackPermissionsIntoString(IEnumerable<Permission> permissions);
        IEnumerable<Permission> UnPackPermissionFromString(string packedPermissions);
    }
}
