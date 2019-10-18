using Microsoft.AspNetCore.Authorization;
using Stml.Infrastructure.Json.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Infrastructure.Authorizations.Permissions
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private IPermissionChecker _permissionChecker;
        public PermissionHandler(IPermissionChecker permissionChecker)
        {
            _permissionChecker = permissionChecker;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var check = _permissionChecker.Check(context.User, requirement.PermissionName);
            if (check)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
