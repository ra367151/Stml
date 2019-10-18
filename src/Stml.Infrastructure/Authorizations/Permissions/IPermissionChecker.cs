using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Infrastructure.Authorizations.Permissions
{
    public interface IPermissionChecker
    {
        bool Check(ClaimsPrincipal user, string permissionName);
    }
}
