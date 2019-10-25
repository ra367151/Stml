using Stml.Infrastructure.Authorizations.Permissions;
using Stml.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Domain.Authorizations
{
    public class RolePermission : IEntity<Guid>
    {
        public RolePermission()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Role Role { get; private set; }
        public Permission Permission { get; set; }
    }
}
