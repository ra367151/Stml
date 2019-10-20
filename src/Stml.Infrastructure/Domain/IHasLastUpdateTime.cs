using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Domain
{
    public interface IHasLastUpdateTime
    {
        DateTime? LastUpdateTime { get; }
    }
}
