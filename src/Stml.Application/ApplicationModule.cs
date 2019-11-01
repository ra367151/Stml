using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.Applications;
using Stml.Infrastructure.DDD.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application
{
    public class ApplicationModule : StmlModule
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // register application services by convension
            //  IUserAppService -> UserAppService
            //  IRoleAppService -> RoleAppService
            //  other application services...
            services.RegisterApplicationServicesByConvension();

            MapCurrentAssembly();
        }

        public override void Configure(IApplicationBuilder app)
        {

        }
    }
}
