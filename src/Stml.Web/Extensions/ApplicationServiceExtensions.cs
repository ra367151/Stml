using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.Domain.Extensions;
using Stml.Infrastructure.Datas;
using Stml.Infrastructure.Dependency;
using System;
using Stml.Infrastructure.Uow.Extensions;
using Stml.Infrastructure.Repository.Extensions;
using Stml.Infrastructure.EPPlus.Extensions;
using Stml.Infrastructure.Applications.Navigation.Extensions;
using Stml.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Stml.Domain.Roles;
using Stml.Web.Startup.Permissions;
using Stml.Infrastructure.Authorizations.Extensions;
using Stml.Infrastructure.Security.Encryption;
using Stml.Web.Startup.Navigations;
using Stml.Infrastructure.Authorizations.Permissions.Extensions;
using Stml.Application;
using Stml.Domain.Repositories;

namespace Stml.Web.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceProvider AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureCoreService();

            services.ConfigureCookiePolicy();
            services.ConfigureMvc();
            services.ConfigureDbContext(config);
            services.ConfigureIdentity();
            services.AddEfCoreRepository();
            services.AddEfCoreUnitOfWork();
            services.ConfigureAutoMapper();
            services.AddExcelManager();
            services.AddNavigationProvider<StmlNavigationProvider>();
            services.AddPermissionProvider<StmlPermissionProvider>();
            services.ConfigureAuthorization<StmlUserClaimsPrincipalFactory, User, Role>();
            return services.UseAutofac(builder =>
            {
                builder.ConfigureApplicationServicesByConvension();
                builder.ConfigureDomainServicesByConvension();
                builder.ConfigureDomainRepositoriesByConvension();
            });
        }

        private static IServiceCollection ConfigureCookiePolicy(this IServiceCollection services)
        {
            return services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        private static IMvcBuilder ConfigureMvc(this IServiceCollection services)
        {
            return services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                        .ConfigureFluentValidaton()
                        .AddControllersAsServices();
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
