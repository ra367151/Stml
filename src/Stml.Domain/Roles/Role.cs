using Microsoft.AspNetCore.Identity;
using Stml.Infrastructure.Authorizations.Permissions;
using Stml.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Domain.Roles
{
    public class Role : IdentityRole<Guid>, IAggregateRoot<Guid>
    {
        public Role()
        {
            Id = Guid.NewGuid();
        }

        public ICollection<RolePermission> Permissions { get; private set; }
    }
}
