using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Uow
{
    public interface IEfCoreUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {

    }
}
