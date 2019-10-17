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
        private readonly IPermissionPacker _permissionPacker;

        public PermissionHandler(IPermissionPacker permissionPacker)
        {
            _permissionPacker = permissionPacker;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var permissionClaim =
                context.User.Claims.SingleOrDefault(c => c.Type == PermissionConstants.PermissionClaimType);
            if (permissionClaim == null)
                return Task.CompletedTask;

            if (_permissionPacker.UnPackPermissionFromString(permissionClaim.Value).Any(p => p.Name == requirement.PermissionName))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
