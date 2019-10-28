using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.EntityFrameworkCore;
using System;
using Stml.Application;
using Stml.Domain;
using Stml.Infrastructure;

namespace Stml.Web.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration config)
        {
            new WebModule().ConfigureServices(services, config);
            new ApplicationModule().ConfigureServices(services, config);
            new DomainModule().ConfigureServices(services, config);
            new EntityFrameworkCoreModule().ConfigureServices(services, config);
            new InfrastructureModule().ConfigureServices(services, config);
        }
    }
}
