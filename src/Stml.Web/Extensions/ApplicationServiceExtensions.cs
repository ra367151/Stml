using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.Application.Extensions;
using Stml.Domain.Extensions;
using Stml.Infrastructure.Datas;
using Stml.Infrastructure.Dependency;
using System;
using Stml.Infrastructure.Uow.Extensions;
using Stml.Infrastructure.Repository.Extensions;
using Stml.Domain.Repositories.Extensions;
using Stml.Infrastructure.EPPlus.Extensions;

namespace Stml.Web.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceProvider AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureCookie();
            services.ConfigureMvc();
            services.ConfigureDbContext(config);
            services.AddEfCoreRepository();
            services.AddEfCoreUnitOfWork();
            services.ConfigureAutoMapper();
            services.AddExcelManager();
            return services.UseAutofac(builder =>
            {
                builder.ConfigureApplicationServicesByConvension();
                builder.ConfigureDomainServicesByConvension();
                builder.ConfigureDomainRepositoriesByConvension();
            });
        }

        private static IServiceCollection ConfigureCookie(this IServiceCollection services)
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
    }
}
