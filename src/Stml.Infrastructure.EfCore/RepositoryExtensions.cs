using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Stml.EntityFrameworkCore
{
    public static class RepositoryExtensions
    {
        public static ContainerBuilder ConfigureRepositoriesByConvension(this ContainerBuilder builder)
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
