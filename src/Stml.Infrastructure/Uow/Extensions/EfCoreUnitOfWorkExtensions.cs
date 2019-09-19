using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Uow.Extensions
{
    public static class EfCoreUnitOfWorkExtensions
    {
        public static IServiceCollection AddEfCoreUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEfCoreUnitOfWork<>), typeof(EfCoreUnitOfWork<>));
            return services;
        }
    }
}
