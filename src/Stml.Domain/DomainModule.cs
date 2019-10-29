using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.Applications;
using Stml.Infrastructure.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Domain
{
    public class DomainModule : StmlModule
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterAssemblyTypes().Where(p => p.Name.EndsWith("DomainService")).AsImplementedInterfaces();
        }

        public override void Configure(IApplicationBuilder app)
        {

        }
    }
}
