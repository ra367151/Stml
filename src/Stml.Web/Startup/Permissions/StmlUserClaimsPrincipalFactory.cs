﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stml.Domain.Authorizations;
using Stml.Infrastructure.Authorizations.Permissions;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Stml.Web.Startup.Permissions
{
    public class StmlUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        private IPermissionPacker _permissionPacker;

        public StmlUserClaimsPrincipalFactory(UserManager<User> userManager
            , RoleManager<Role> roleManager
            , IOptions<IdentityOptions> options
            , IPermissionPacker permissionPacker)
            : base(userManager, roleManager, options)
        {
            _permissionPacker = permissionPacker;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var roleNames = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            var permissions = await (RoleManager.Roles
                                    .Where(r => roleNames.Contains(r.Name))
                                    .AsNoTracking()).SelectMany(x => x.Permissions).ToListAsync();
            identity.AddClaim(new Claim(PermissionConstants.PermissionClaimType, _permissionPacker.PackPermissionsIntoString(permissions)));
            return identity;
        }
    }
}
