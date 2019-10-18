using Microsoft.AspNetCore.Identity;
using Stml.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Domain.Users
{
    public class User : IdentityUser<Guid>, IAggregateRoot<Guid>
    {
        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
