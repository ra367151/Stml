using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
        protected readonly IMapper _mapper;

        public StmlController(IPermissionPacker permissionPacker
            , IPermissionManager<Permission> permissionManager
            , IMapper mapper)
        {
            _permissionPacker = permissionPacker;
            _permissionManager = permissionManager;
            _mapper = mapper;
        }

        [Ajax(Http.Get)]
        public IEnumerable<string> GetUserPermissions()
        {
            if (User.IsInRole(RoleConstants.DefaultAdminRoleName))
                return _permissionManager.Permissions.SelectMany(x => x.Value).Select(x => x.Name);

            var packedPermissions = User.Claims?.SingleOrDefault(x => x.Type == PermissionConstants.PermissionClaimType);
            return packedPermissions == null ? null : _permissionPacker.UnPackPermissionFromString<Permission>(packedPermissions.Value)?.Select(x => x.Name);
        }
    }
}