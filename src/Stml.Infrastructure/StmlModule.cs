using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure
{
    public abstract class StmlModule
    {
        public abstract void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    }
}
