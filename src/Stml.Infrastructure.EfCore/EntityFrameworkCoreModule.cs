using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.Applications;
using Stml.Infrastructure.DDD.Repository.Extensions;
using Stml.Infrastructure.DDD.Uow.Extensions;
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

            // register repositories:
            //  IRepository<User,Guid> -> UserRepository
            //  IUserRepository -> UserRepository
            //  IRepository<Role,Guid> -> RoleRepository
            //  IRoleRepository -> RoleRepository
            //  other services...
            services.RegisterRepositoriesByConvension();

            // register unitofwork:
            //  IEfCoreUnitOfWork<StmlDbContext> -> EfCoreUnitOfWork<StmlDbContext>
            services.AddEfCoreUnitOfWork<StmlDbContext>();
        }

        public override void Configure(IApplicationBuilder app)
        {

        }
    }
}
