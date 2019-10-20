using Microsoft.AspNetCore.Identity;
using Stml.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Domain.Users
{
    public class User : IdentityUser<Guid>, IAggregateRoot<Guid>, IHasCreationTime, IHasLastUpdateTime
    {
        public User()
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.Now;
            IsEnable = true;
        }

        public bool IsEnable { get; private set; }

        public DateTime CreationTime { get; private set; }

        public DateTime? LastUpdateTime { get; private set; }


        public User Update()
        {
            LastUpdateTime = DateTime.Now;
            return this;
        }

        public User Enable(bool isEnable = true)
        {
            IsEnable = isEnable;
            return this;
        }
    }
}
