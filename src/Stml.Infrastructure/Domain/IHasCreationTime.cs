using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Domain
{
    public interface IHasCreationTime
    {
        DateTime CreationTime { get; }
    }
}
