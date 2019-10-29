using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.Application;
using Stml.Domain;
using Stml.EntityFrameworkCore;
using Stml.Infrastructure;
using Stml.Infrastructure.DependencyInjection.Extensions;
using System;

namespace Stml.Web.Startup
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Start<WebModule>()
                    .Then<ApplicationModule>()
                    .Then<DomainModule>()
                    .Then<EntityFrameworkCoreModule>()
                    .Then<InfrastructureModule>()
                    .ConfigureService(Configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Configure();
        }
    }
}
