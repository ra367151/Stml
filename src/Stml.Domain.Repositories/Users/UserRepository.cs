using Microsoft.EntityFrameworkCore;
using Stml.Domain.Authorizations;
using Stml.Infrastructure.Datas;
using Stml.Infrastructure.Linq.Extensions;
using Stml.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Domain.Repositories.Users
{
    public class UserRepository : EfCoreRepository<StmlDbContext, User, Guid>, IUserRepository
    {
        public UserRepository(StmlDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> FindUserIncludeRolesAsync(Guid id)
        {
            return await _dbContext.Users
                                .Include(u => u.UserRoles)
                                    .ThenInclude(ur => ur.Role)
                                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsersIncludeRolesAsync(string search, int skip, int take)
        {
            return await _dbContext.Users
                                .WhereIf(!string.IsNullOrEmpty(search), u => u.UserName.Contains(search) || u.Email.Contains(search))
                                .Include(u => u.UserRoles)
                                    .ThenInclude(ur => ur.Role)
                                .OrderByDescending(u => u.CreationTime)
                                .Skip(skip)
                                .Take(take)
                                .ToListAsync();
        }
    }
}
