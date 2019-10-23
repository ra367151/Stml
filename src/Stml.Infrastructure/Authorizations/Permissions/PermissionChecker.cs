using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Infrastructure.Authorizations.Permissions
{
    public class PermissionChecker : IPermissionChecker
    {
        private readonly IPermissionPacker _permissionPacker;

        public PermissionChecker(IPermissionPacker permissionPacker)
        {
            _permissionPacker = permissionPacker;
        }

        public bool Check(ClaimsPrincipal user, string permissionName)
        {
            var permissionClaim = user.Claims.SingleOrDefault(c => c.Type == PermissionConstants.PermissionClaimType);
            return permissionClaim != null
                &&
                (_permissionPacker.UnPackPermissionFromString(permissionClaim.Value).Any(p => p.Name == permissionName) || user.IsInRole(PermissionConstants.SuperAdminRoleName));
        }
    }
}
