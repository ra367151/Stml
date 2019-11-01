using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Stml.Infrastructure.DDD.Domain.Extensions
{
    public static class DomainExtensions
    {
        public static IServiceCollection RegisterDomainServicesByConvension(this IServiceCollection services)
        {
            services.RegisterAssemblyTypes(Assembly.GetCallingAssembly()).Where(p => p.Name.EndsWith("DomainService")).AsImplementedInterfaces();
            return services;
        }
    }
}
