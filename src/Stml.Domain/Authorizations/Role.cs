using Microsoft.AspNetCore.Identity;
using Stml.Infrastructure.Authorizations.Permissions;
using Stml.Infrastructure.DDD.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Domain.Authorizations
{
    public class Role : IdentityRole<Guid>, IAggregateRoot<Guid>
    {
        public Role()
        {
            Id = Guid.NewGuid();
        }

        public string DisplayName { get; private set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<RolePermission> Permissions { get; set; }

        public static Role CreateAdministratorRole()
        {
            var role = new Role
            {
                Name = RoleConstants.DefaultAdminRoleName,
                DisplayName = RoleConstants.DefaultAdminRoleDisplayName
            };
            return role;
        }

        public static Role CreateRole(string name, string displayName = null)
        {
            return new Role
            {
                Name = name,
                DisplayName = displayName ?? name
            };
        }
    }
}
