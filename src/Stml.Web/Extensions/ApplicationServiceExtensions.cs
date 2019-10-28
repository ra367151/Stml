using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.EntityFrameworkCore;
using System;
using Stml.Infrastructure.EPPlus.Extensions;
using Stml.Infrastructure.Applications.Navigation.Extensions;
using Microsoft.AspNetCore.Identity;
using Stml.Web.Startup.Permissions;
using Stml.Infrastructure.Authorizations.Extensions;
using Stml.Infrastructure.Security.Encryption;
using Stml.Web.Startup.Navigations;
using Stml.Infrastructure.Authorizations.Permissions.Extensions;
using Stml.Application;
using Stml.Domain.Authorizations;

namespace Stml.Web.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureCoreService();

            services.ConfigureCookiePolicy();
            services.ConfigureMvc();
            services.ConfigureDbContext(config);
            services.ConfigureIdentity();
            services.ConfigureAutoMapper();
            services.AddExcelManager();
            services.AddNavigationProvider<StmlNavigationProvider>();
            services.AddPermissionProvider<StmlPermissionProvider>();
            services.ConfigureAuthorization<StmlUserClaimsPrincipalFactory, User, Role>();
        }

        private static IServiceCollection ConfigureCookiePolicy(this IServiceCollection services)
        {
            return services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        private static void ConfigureMvc(this IServiceCollection services)
        {
            services.AddControllersWithViews().AddControllersAsServices();
        }

        private static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration config)
        {
            return services.AddDbContext<StmlDbContext>(options => options.UseSqlServer(config.GetConnectionString("Stml")));
        }

        private static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<StmlDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        private static IServiceCollection ConfigureCoreService(this IServiceCollection services)
        {
            services.AddSingleton<IStringEncryptionService, StringEncryptionService>()
                .Configure<StringEncryptionOptions>(options => { });

            return services;
        }
    }
}
