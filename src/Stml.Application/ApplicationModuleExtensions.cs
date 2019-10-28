using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Stml.Application
{
    public static class ApplicationModuleExtensions
    {
        public static IServiceCollection ConfigureApplicationModuleServices(this IServiceCollection services)
        {
            return services.RegisterAssemblyTypes()
                        .Where(t => t.Name.EndsWith("AppService"))
                        .AsImplementedInterfaces();
        }
    }
}
