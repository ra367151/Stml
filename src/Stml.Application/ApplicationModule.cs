using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.Applications;
using Stml.Infrastructure.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application
{
    public class ApplicationModule : StmlModule
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterAssemblyTypes().Where(p => p.Name.EndsWith("AppService")).AsImplementedInterfaces();

            MapCurrentAssembly();
        }

        public override void Configure(IApplicationBuilder app)
        {

        }
    }
}
