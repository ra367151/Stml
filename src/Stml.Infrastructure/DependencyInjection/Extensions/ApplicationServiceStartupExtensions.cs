using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.Applications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.DependencyInjection.Extensions
{
    public static class ApplicationServiceStartupExtensions
    {
        public static IApplicationContext Start<TStartModule>(this IServiceCollection services) where TStartModule : StmlModule
        {
            var context = new ApplicationContext();
            context.AddStartModule<TStartModule>(services);
            services.AddSingleton(typeof(IApplicationContext), context);
            return context;
        }
    }
}
