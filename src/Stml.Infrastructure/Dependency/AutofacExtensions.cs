using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Stml.Infrastructure.Dependency
{
    public static class AutofacExtensions
    {
        public static IServiceProvider UseAutofac(this IServiceCollection services, Action<ContainerBuilder> containerBuilder = null)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            var controllerTypesInAssembly = Assembly.GetEntryAssembly().GetExportedTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();
            builder.RegisterTypes(controllerTypesInAssembly).PropertiesAutowired();
            containerBuilder?.Invoke(builder);
            return builder.Build().Resolve<IServiceProvider>();
        }
    }
}
