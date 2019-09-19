using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Domain
{
    internal interface IAggregateRoot : IAggregateRoot<int>
    {
    }

    internal interface IAggregateRoot<TKey> : IEntity<TKey>
    {

    }
}
