using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stml.EntityFrameworkCore.Repositories;
using Stml.Infrastructure.DDD.Uow;
using Stml.Infrastructure.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Stml.EntityFrameworkCore
{
    public static class EntityFrameworkCoreServiceExtensions
    {
        public static IServiceCollection ConfigureEntityFrameworkCoreModuleServices(this IServiceCollection services)
        {
            return services.RegisterGenericRepository()
                        .RegisterRepositoriesByConvention()
                        .RegisterUnitOfWork<StmlDbContext>();
        }

        private static IServiceCollection RegisterGenericRepository(this IServiceCollection services)
        {
            return services.AddTransient(typeof(StmlRepositoryBase<,>));
        }

        private static IServiceCollection RegisterRepositoriesByConvention(this IServiceCollection services)
        {
            return services.RegisterAssemblyTypes()
                        .Where(p => p.Name.EndsWith("Repository"))
                        .AsImplementedInterfaces();
        }

        private static IServiceCollection RegisterUnitOfWork<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            return services.AddScoped(typeof(IEfCoreUnitOfWork<TDbContext>), typeof(EfCoreUnitOfWork<TDbContext>));
        }
    }
}
