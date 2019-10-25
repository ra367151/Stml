using Autofac;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Stml.Application
{
    public static class ApplicationModuleExtensions
    {
        public static void ConfigureApplicationModuleServices(this ContainerBuilder builder)
        {
            builder.ConfigureApplicationServicesByConvension();
        }

        public static IMvcBuilder ConfigureFluentValidaton(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
        }

        private static ContainerBuilder ConfigureApplicationServicesByConvension(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(p => p.Name.EndsWith("AppService"))
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired();
            return builder;
        }
    }
}
