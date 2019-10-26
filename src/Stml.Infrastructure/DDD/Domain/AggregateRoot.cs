using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.DDD.Domain
{
    public class AggregateRoot : AggregateRoot<int>
    {

    }

    public class AggregateRoot<TKey> : Entity<TKey>
    {

    }
}
