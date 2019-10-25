using Stml.Domain.Authorizations;
using Stml.Infrastructure.Datas;
using Stml.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Domain.Repositories.Users
{
    public interface IUserRepository : IEfCoreRepository<StmlDbContext, User, Guid>
    {
        Task<IEnumerable<User>> GetUsersIncludeRolesAsync(string search, int skip, int take);
        Task<User> FindUserIncludeRolesAsync(Guid id);
    }
}
