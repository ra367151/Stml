using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.Applications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.DependencyInjection
{
    public interface IApplicationContext
    {
        IServiceCollection Services { get; }
        IReadOnlyList<StmlModule> ModuleTypes { get; }
        void AddStartModule<TModule>(IServiceCollection services) where TModule : StmlModule;
        IApplicationContext Then<TModule>() where TModule : StmlModule;
        IServiceCollection ConfigureService(IConfiguration configuration);
        IApplicationBuilder Configure(IApplicationBuilder app);
    }
}
