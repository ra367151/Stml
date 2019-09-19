using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Domain
{
    internal interface IEntity : IEntity<int>
    {

    }

    internal interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}
