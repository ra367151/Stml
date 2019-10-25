using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stml.EntityFrameworkCore.Repositories;
using Stml.Infrastructure;
using Stml.Infrastructure.Uow;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Stml.EntityFrameworkCore
{
    public static class EntityFrameworkCoreServiceExtensions
    {
        public static ContainerBuilder ConfigureEntityFrameworkCoreModuleServices(this ContainerBuilder builder)
        {
            builder.RegisterGenericRepository();
            builder.RegisterRepositoriesByConvention();
            builder.RegisterUnitOfWork<StmlDbContext>();
            return builder;
        }

        private static void RegisterGenericRepository(this ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(StmlRepositoryBase<,>)).AsImplementedInterfaces();
        }

        private static void RegisterRepositoriesByConvention(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                            .Where(p => p.Name.EndsWith("Repository"))
                            .AsImplementedInterfaces()
                            .InstancePerDependency()
                            .PropertiesAutowired();
        }

        private static void RegisterUnitOfWork<TDbContext>(this ContainerBuilder builder) where TDbContext : DbContext
        {
            builder.RegisterType(typeof(EfCoreUnitOfWork<TDbContext>)).As(typeof(EfCoreUnitOfWork<TDbContext>));
        }
    }
}
