using Microsoft.AspNetCore.Identity;
using Stml.Infrastructure.Applications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Domain.Authorizations
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public UserRole() { }
        public UserRole(User user, Role role) : this()
        {
            Check.NotNull(user, nameof(user));
            Check.NotNull(role, nameof(role));
            User = user;
            Role = role;
        }

        public virtual User User { get; private set; }
        public virtual Role Role { get; private set; }
    }
}
