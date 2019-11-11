using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stml.Domain.Authorizations;
using Stml.EntityFrameworkCore;
using Stml.Infrastructure.Applications.Navigation.Extensions;
using Stml.Infrastructure.Authorizations.Extensions;
using Stml.Infrastructure.Authorizations.Permissions.Extensions;
using Stml.Infrastructure.Applications;
using Stml.Web.Startup.Navigations;
using Stml.Web.Startup.Permissions;

namespace Stml.Web
{
    public class WebModule : StmlModule
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<StmlDbContext>()
            .AddDefaultTokenProviders();

            services.AddNavigationProvider<StmlNavigationProvider>();
            services.AddPermissionProvider<StmlPermissionProvider>();
            services.ConfigureAuthorization<StmlUserClaimsPrincipalFactory, User, Role>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            MapCurrentAssembly();
        }

        public override void Configure(IApplicationBuilder app)
        {
            ConfigureCoreMiddleware(app);
            ConfigureNavigationProvider(app);
            ConfigurePermissionProvider(app);
        }

        #region ConfigureCoreMiddleware
        private void ConfigureCoreMiddleware(IApplicationBuilder app)
        {
            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        #endregion

        #region ConfigureNavigationProvider
        private void ConfigureNavigationProvider(IApplicationBuilder app)
        {
            app.UseNavigationProvider<StmlNavigationProvider>();
        }
        #endregion

        #region ConfigurePermissionProvider
        private void ConfigurePermissionProvider(IApplicationBuilder app)
        {
            app.UsePermissionProvider<StmlPermissionProvider>();
        }
        #endregion
    }
}
