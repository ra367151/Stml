using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.Application;
using Stml.Domain.Authorizations;
using Stml.EntityFrameworkCore;
using Stml.Infrastructure;
using Stml.Infrastructure.Applications.Navigation.Extensions;
using Stml.Infrastructure.Authorizations.Extensions;
using Stml.Infrastructure.Authorizations.Permissions.Extensions;
using Stml.Web.Startup.Navigations;
using Stml.Web.Startup.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Stml.Web
{
    public class WebModule : StmlModule
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureCookiePolicy(services);
            ConfigureMvc(services);
            ConfigureIdentity(services);
            //ConfigureAutoMapper(services);

            var mappintConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new WebMapProfile());
                mc.AddProfile(new ApplicationMapProfile());
            });
            services.AddSingleton(mappintConfig.CreateMapper());


            services.AddNavigationProvider<StmlNavigationProvider>();
            services.AddPermissionProvider<StmlPermissionProvider>();
            services.ConfigureAuthorization<StmlUserClaimsPrincipalFactory, User, Role>();
        }

        private void ConfigureCookiePolicy(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        private void ConfigureMvc(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        private void ConfigureIdentity(IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<StmlDbContext>()
            .AddDefaultTokenProviders();
        }

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
