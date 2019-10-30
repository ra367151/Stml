using Microsoft.AspNetCore.Identity;
using Stml.Infrastructure.Authorizations.Permissions;
using Stml.Infrastructure.DDD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stml.Domain.Authorizations
{
    public class User : IdentityUser<Guid>, IAggregateRoot<Guid>, IHasCreationTime, IHasLastUpdateTime
    {
        public User()
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.Now;
            IsActive = true;
            UserRoles = new List<UserRole>();
        }

        public bool IsActive { get; private set; }

        public DateTime CreationTime { get; private set; }

        public DateTime? LastUpdateTime { get; private set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public static User CreateAdministrator()
        {
            var user = new User
            {
                UserName = UserConstants.DefaultAdminUserName,
                Email = $"{UserConstants.DefaultAdminUserName}{UserConstants.DefaultEmailAddressSuffix}"
            };
            return user;
        }

        public static User CreateUser(string username, string email, bool isActive = true)
        {
            var user = new User
            {
                UserName = username,
                Email = email,
                IsActive = isActive
            };
            return user;
        }

        public User AddToRoles(params Role[] roles)
        {
            UserRoles = roles.Select(r => new UserRole(this, r)).ToList();
            return this;
        }

        public User UpdateUserName(string username)
        {
            UserName = username;
            LastUpdateTime = DateTime.Now;
            return this;
        }

        public User UpdateEmail(string email)
        {
            Email = email;
            LastUpdateTime = DateTime.Now;
            return this;
        }

        public User UpdateState(bool isActive)
        {
            IsActive = isActive;
            LastUpdateTime = DateTime.Now;
            return this;
        }
    }
}
