using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Domain
{
    public interface IAggregateRoot : IAggregateRoot<int>
    {
    }

    public interface IAggregateRoot<TKey> : IEntity<TKey>
    {

    }
}
