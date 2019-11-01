using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.DDD.Repository.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection RegisterRepositoriesByConvension(this IServiceCollection services)
        {
            services.RegisterAssemblyTypes().Where(p => p.Name.EndsWith("Repository")).AsImplementedInterfaces();
            return services;
        }
    }
}
