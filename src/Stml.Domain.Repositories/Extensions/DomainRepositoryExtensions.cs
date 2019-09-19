using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Stml.Domain.Repositories.Extensions
{
    public static class DomainRepositoryExtensions
    {
        public static ContainerBuilder ConfigureDomainRepositoriesByConvension(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(p => p.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired();
            return builder;
        }
    }
}
