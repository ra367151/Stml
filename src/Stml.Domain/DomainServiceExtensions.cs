using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.DependencyInjection.Extensions;
using System.Reflection;

namespace Stml.Domain
{
    public static class DomainServiceExtensions
    {
        public static IServiceCollection ConfigureDomainModuleServices(this IServiceCollection services)
        {
            return services.RegisterAssemblyTypes()
                        .Where(p => p.Name.EndsWith("DomainService"))
                        .AsImplementedInterfaces();
        }
    }
}
