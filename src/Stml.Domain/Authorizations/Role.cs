using Microsoft.AspNetCore.Identity;
using Stml.Infrastructure.Authorizations.Permissions;
using Stml.Infrastructure.DDD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stml.Domain.Authorizations
{
    public class Role : IdentityRole<Guid>, IAggregateRoot<Guid>
    {
        public Role()
        {
            Id = Guid.NewGuid();
            UserRoles = new List<UserRole>();
            Permissions = new List<Permission>();
        }

        public string DisplayName { get; private set; }

        public virtual ICollection<UserRole> UserRoles { get; private set; }

        public ICollection<Permission> Permissions { get; private set; }

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

        public Role SetPermissionsToRole(params Permission[] permissions)
        {
            Permissions = permissions.ToList();
            return this;
        }

        public Role UpdateName(string name)
        {
            Name = name;
            return this;
        }

        public Role UpdateDisplayName(string displayName)
        {
            DisplayName = displayName;
            return this;
        }
    }
}
