using Microsoft.EntityFrameworkCore;
using Stml.Infrastructure.DDD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Infrastructure.DDD.Repository
{
    public class EfCoreRepository<TDbContext, TAggregateRoot> : EfCoreRepository<TDbContext, TAggregateRoot, int>
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot
    {
        public EfCoreRepository(TDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class EfCoreRepository<TDbContext, TAggregateRoot, TKey> : IRepository<TAggregateRoot, TKey>
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        protected readonly TDbContext _dbContext;

        public EfCoreRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TAggregateRoot obj)
        {
            _dbContext.Add(obj);
        }

        public void AddRange(IEnumerable<TAggregateRoot> objs)
        {
            _dbContext.AddRange(objs);
        }

        public IEnumerable<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return _dbContext.Set<TAggregateRoot>().Where(predicate).ToList();
        }

        public async Task<IEnumerable<TAggregateRoot>> GetAsync(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return await _dbContext.Set<TAggregateRoot>().Where(predicate).ToListAsync();
        }

        public TAggregateRoot Find(TKey id)
        {
            return _dbContext.Find<TAggregateRoot>(id);
        }

        public async Task<TAggregateRoot> FindAsync(TKey id)
        {
            return await _dbContext.FindAsync<TAggregateRoot>(id);
        }

        public IEnumerable<TAggregateRoot> GetAll()
        {
            return _dbContext.Set<TAggregateRoot>().ToList();
        }

        public async Task<IEnumerable<TAggregateRoot>> GetAllAsync()
        {
            return await _dbContext.Set<TAggregateRoot>().ToListAsync();
        }

        public void Remove(TAggregateRoot obj)
        {
            _dbContext.Remove(obj);
        }

        public void RemoveRange(IEnumerable<TAggregateRoot> objs)
        {
            _dbContext.RemoveRange(objs);
        }
    }
}
