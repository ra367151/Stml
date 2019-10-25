using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Stml.Infrastructure.Applications.MVC.Http;
using Stml.Infrastructure.Authorizations.Permissions;
using static System.Net.WebRequestMethods;

namespace Stml.Web.Controllers
{
    public class StmlController : Controller
    {
        protected readonly IPermissionPacker _permissionPacker;
        protected readonly IPermissionManager<Permission> _permissionManager;

        public StmlController(IPermissionPacker permissionPacker, IPermissionManager<Permission> permissionManager)
        {
            _permissionPacker = permissionPacker;
            _permissionManager = permissionManager;
        }

        [Ajax(Http.Get)]
        public IEnumerable<string> GetUserPermissions()
        {
            if (User.IsInRole(RoleConstants.DefaultAdminRoleName))
                return _permissionManager.Permissions.SelectMany(x => x.Value).Select(x => x.Name);

            var packedPermissions = User.Claims?.SingleOrDefault(x => x.Type == PermissionConstants.PermissionClaimType);
            return packedPermissions == null ? null : _permissionPacker.UnPackPermissionFromString(packedPermissions.Value)?.Select(x => x.Name);
        }


    }
}