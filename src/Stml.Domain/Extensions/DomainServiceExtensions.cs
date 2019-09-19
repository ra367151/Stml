using Autofac;
using System.Reflection;

namespace Stml.Domain.Extensions
{
    public static class DomainServiceExtensions
    {
        public static ContainerBuilder ConfigureDomainServicesByConvension(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(p => p.Name.EndsWith("DomainService"))
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired();
            return builder;
        }
    }
}
