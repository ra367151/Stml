//using Stml.Infrastructure.Applications;
//using Stml.Infrastructure.Authorizations.Permissions;
//using Stml.Infrastructure.DDD.Domain;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Stml.Domain.Authorizations
//{
//    public class RolePermission : IEntity<Guid>
//    {
//        public RolePermission()
//        {
//            Id = Guid.NewGuid();
//        }

//        public RolePermission(Role role, Permission permission)
//            : this()
//        {
//            Check.NotNull(role, nameof(role));
//            Check.NotNull(permission, nameof(permission));
//            RoleId = role.Id;
//            Role = role;
//            Permission = permission;
//        }

//        public Guid Id { get; private set; }
//        public Guid RoleId { get; private set; }
//        public Role Role { get; private set; }
//        public Permission Permission { get; private set; }
//    }
//}
