using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Domain
{
    public interface IEntity : IEntity<int>
    {

    }

    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}
