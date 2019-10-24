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
            IsActive = true;
        }

        public bool IsActive { get; private set; }

        public DateTime CreationTime { get; private set; }

        public DateTime? LastUpdateTime { get; private set; }

        public User UpdateUserName(string username)
        {
            UserName = username;
            return Update();
        }

        public User UpdateEmai(string email)
        {
            Email = email;
            return Update();
        }

        public User UpdateToActive()
        {
            IsActive = true;
            return Update();
        }

        public User UpdateToUnActive()
        {
            IsActive = false;
            return Update();
        }

        public User Update()
        {
            LastUpdateTime = DateTime.Now;
            return this;
        }
    }
}
