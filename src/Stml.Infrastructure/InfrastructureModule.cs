using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.EPPlus.Extensions;
using Stml.Infrastructure.Security.Encryption;

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
    }
}
