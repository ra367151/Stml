using Microsoft.EntityFrameworkCore;
using Stml.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Repository
{
    public interface IEfCoreRepository<TDbContext, TAggregateRoot> : IEfCoreRepository<TDbContext, TAggregateRoot, int>
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot
    {

    }

    public interface IEfCoreRepository<TDbContext, TAggregateRoot, TKey> : IRepository<TAggregateRoot, TKey>
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {

    }
}
