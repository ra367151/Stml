using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.Application;
using Stml.Domain;
using Stml.EntityFrameworkCore;
using Stml.Infrastructure.Applications.Navigation.Extensions;
using Stml.Infrastructure.Authorizations.Permissions.Extensions;
using Stml.Web.Extensions;
using Stml.Web.Startup.Navigations;
using Stml.Web.Startup.Permissions;
using System;

namespace Stml.Web.Startup
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; private set; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.ConfigureApplicationModuleServices();
            builder.ConfigureDomainModuleServices();
            builder.ConfigureEntityFrameworkCoreModuleServices();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureApplication(env)
                .UseNavigationProvider<StmlNavigationProvider>()
                .UsePermissionProvider<StmlPermissionProvider>();
        }

        //public IServiceProvider ConfigureServices(IServiceCollection services)
        //{
        //    return services.AddApplication(Configuration);
        //}

        //public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration cfg)
        //{
        //    app.ConfigureApplication(env, cfg)
        //        .UseNavigationProvider<StmlNavigationProvider>()
        //        .UsePermissionProvider<StmlPermissionProvider>();
        //}
    }
}
