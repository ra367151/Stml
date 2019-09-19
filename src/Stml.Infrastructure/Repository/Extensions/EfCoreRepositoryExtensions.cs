using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Repository.Extensions
{
    public static class EfCoreRepositoryExtensions
    {
        public static IServiceCollection AddEfCoreRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEfCoreRepository<,,>), typeof(EfCoreRepository<,,>));
            return services;
        }
    }
}
