using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.Applications;
using Stml.Infrastructure.EPPlus.Extensions;
using Stml.Infrastructure.System.Security.Encryption;

namespace Stml.Infrastructure
{
    public class InfrastructureModule : StmlModule
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IStringEncryptionService, StringEncryptionService>()
                .Configure<StringEncryptionOptions>(options => { });

            services.AddExcelManager();
        }

        public override void Configure(IApplicationBuilder app)
        {

        }
    }
}
