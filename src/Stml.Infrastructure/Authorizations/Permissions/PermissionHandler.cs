using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Infrastructure.Authorizations.Permissions
{
    public class PermissionHandler<TPermission> : AuthorizationHandler<PermissionRequirement> where TPermission : Permission
    {
        private IPermissionChecker _permissionChecker;
        public PermissionHandler(IPermissionChecker permissionChecker)
        {
            _permissionChecker = permissionChecker;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var check = _permissionChecker.Check<TPermission>(context.User, requirement.PermissionName);
            if (check)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
