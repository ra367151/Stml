using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications
{
    public abstract class StmlModule
    {
        public bool IsMapCurrentAssembly { get; private set; }

        public StmlModule()
        {
            IsMapCurrentAssembly = false;
        }

        public void MapCurrentAssembly()
        {
            IsMapCurrentAssembly = true;
        }

        public abstract void ConfigureServices(IServiceCollection services, IConfiguration configuration);
        public abstract void Configure(IApplicationBuilder app);
    }
}
