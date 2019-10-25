using Stml.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Infrastructure.Repository
{
    public interface IRepository<TAggregateRoot> : IRepository<TAggregateRoot, int>
        where TAggregateRoot : class, IAggregateRoot
    {

    }

    public interface IRepository<TAggregateRoot, TKey> where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        void Add(TAggregateRoot obj);
        void AddRange(IEnumerable<TAggregateRoot> objs);
        void Remove(TAggregateRoot obj);
        void RemoveRange(IEnumerable<TAggregateRoot> objs);
        TAggregateRoot Find(TKey id);
        Task<TAggregateRoot> FindAsync(TKey id);
        IEnumerable<TAggregateRoot> GetAll();
        Task<IEnumerable<TAggregateRoot>> GetAllAsync();
        IEnumerable<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> predicate);
        Task<IEnumerable<TAggregateRoot>> GetAsync(Expression<Func<TAggregateRoot, bool>> predicate);
    }
}
