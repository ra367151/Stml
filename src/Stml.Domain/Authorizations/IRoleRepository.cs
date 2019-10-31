using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Domain.Authorizations
{
    public interface IRoleRepository
    {
        Task<Role> FindRoleIncludeUsersAsync(Guid id);
    }
}
