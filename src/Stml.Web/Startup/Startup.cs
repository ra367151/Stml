using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.Applications.Navigation.Extensions;
using Stml.Web.Extensions;
using Stml.Web.Startup.Navigations;
using System;

namespace Stml.Web.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.AddApplication(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration cfg)
        {
            app.ConfigureApplication(env, cfg)
                .UseNavigationProvider<StmlNavigationProvider>();
        }
    }
}
