using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Domain
{
    public class AggregateRoot : AggregateRoot<int>
    {

    }

    public class AggregateRoot<TKey> : Entity<TKey>
    {

    }
}
