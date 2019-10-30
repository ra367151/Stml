using Microsoft.EntityFrameworkCore;
using Stml.Domain.Authorizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stml.EntityFrameworkCore.Repositories
{
    public class RoleRepository : StmlRepositoryBase<Role, Guid>, IRoleRepository
    {
        public RoleRepository(StmlDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Role> FindRoleIncludePermissionsAndUsersAsync(Guid id)
        {
            return await _dbContext.Roles
                                .Include(r => r.Permissions)
                                    .ThenInclude(rp => rp.Permission)
                                .Include(r => r.Permissions)
                                    .ThenInclude(rp => rp.Role)
                                .Include(r => r.UserRoles)
                                    .ThenInclude(ur => ur.User)
                                .Where(r => r.Id == id).SingleOrDefaultAsync();
        }
    }
}
