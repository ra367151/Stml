using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Uow.Extensions
{
    public static class UnitOfWorkExtensions
    {
        public static IServiceCollection AddEfCoreUnitOfWork<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            services.AddScoped(typeof(IEfCoreUnitOfWork<TDbContext>), typeof(EfCoreUnitOfWork<TDbContext>));
            return services;
        }
    }
}
