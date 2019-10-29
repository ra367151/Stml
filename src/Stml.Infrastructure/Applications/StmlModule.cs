using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications
{
    public abstract class StmlModule
    {
        public IList<Type> Maps { get; }
        public void AddMap<TProfile>() where TProfile : Profile
        {
            Maps.Add(typeof(TProfile));
        }
        public void AddMaps(params Type[] profileTypes)
        {
            Check.NotNull(profileTypes, nameof(profileTypes));
            foreach (var profileType in profileTypes)
            {
                Maps.Add(profileType);
            }
        }

        public StmlModule()
        {
            Maps = new List<Type>();
        }

        public abstract void ConfigureServices(IServiceCollection services, IConfiguration configuration);
        public abstract void Configure(IApplicationBuilder app);
    }
}
