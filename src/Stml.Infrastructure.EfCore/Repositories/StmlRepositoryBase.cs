using Stml.Infrastructure.DDD.Domain;
using Stml.Infrastructure.DDD.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.EntityFrameworkCore.Repositories
{
    public class StmlRepositoryBase<TAggregateRoot, TKey> : EfCoreRepository<StmlDbContext, TAggregateRoot, TKey>
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        public StmlRepositoryBase(StmlDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class StmlRepositoryBase<TAggregateRoot> : StmlRepositoryBase<TAggregateRoot, int>
        where TAggregateRoot : class, IAggregateRoot<int>
    {
        public StmlRepositoryBase(StmlDbContext dbContext) : base(dbContext)
        {
        }
    }
}
