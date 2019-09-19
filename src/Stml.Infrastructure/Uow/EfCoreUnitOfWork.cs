using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Infrastructure.Uow
{
    public class EfCoreUnitOfWork<TDbContext> : IEfCoreUnitOfWork<TDbContext>
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public EfCoreUnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
