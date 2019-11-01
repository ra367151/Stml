using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.DDD.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection RegisterApplicationServicesByConvension(this IServiceCollection services)
        {
            services.RegisterAssemblyTypes().Where(p => p.Name.EndsWith("AppService")).AsImplementedInterfaces();
            return services;
        }
    }
}
