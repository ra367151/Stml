using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.EntityFrameworkCore.Repositories;
using Stml.Infrastructure.Applications;
using Stml.Infrastructure.DDD.Uow;
using Stml.Infrastructure.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.EntityFrameworkCore
{
    public class EntityFrameworkCoreModule : StmlModule
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StmlDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Stml")));

            services.AddTransient(typeof(StmlRepositoryBase<,>));
            services.RegisterAssemblyTypes().Where(p => p.Name.EndsWith("Repository")).AsImplementedInterfaces();
            services.AddScoped(typeof(IEfCoreUnitOfWork<StmlDbContext>), typeof(EfCoreUnitOfWork<StmlDbContext>));
        }

        public override void Configure(IApplicationBuilder app)
        {

        }
    }
}
