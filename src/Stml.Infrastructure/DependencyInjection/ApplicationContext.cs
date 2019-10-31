using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stml.Infrastructure.Applications;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Stml.Infrastructure.DependencyInjection
{
    public class ApplicationContext : IApplicationContext
    {
        private readonly List<StmlModule> _modules;

        public IReadOnlyList<StmlModule> ModuleTypes => _modules.ToImmutableList();

        public IServiceCollection Services { get; private set; }

        public ApplicationContext()
        {
            _modules = new List<StmlModule>();
        }

        public void AddStartModule<TModule>(IServiceCollection services) where TModule : StmlModule
        {
            if (_modules.Any())
                throw new NotImplementedException($"it's already has start module.");
            if (!typeof(StmlModule).IsAssignableFrom(typeof(TModule)))
                throw new ArgumentException($"type {typeof(TModule).Name} is not inherit from {typeof(StmlModule).Name}.");
            if (!_modules.Any(x => x.GetType() == typeof(TModule)))
                _modules.Add(Activator.CreateInstance<TModule>());
            Services = services;
        }

        public IApplicationContext Then<TModule>() where TModule : StmlModule
        {
            if (!_modules.Any())
                throw new NotImplementedException("without a start module.");
            if (!typeof(StmlModule).IsAssignableFrom(typeof(TModule)))
                throw new ArgumentException($"type {typeof(TModule).Name} is not inherit from {typeof(StmlModule).Name}.");
            if (!_modules.Any(x => x.GetType() == typeof(TModule)))
                _modules.Add(Activator.CreateInstance<TModule>());
            return this;
        }

        public IServiceCollection ConfigureService(IConfiguration configuration)
        {
            foreach (var module in _modules)
            {
                module.ConfigureServices(Services, configuration);
            }
            Services.AddAutoMapper(_modules.Where(x => x.IsMapCurrentAssembly).Select(m => m.GetType().Assembly).ToArray());
            return Services;
        }

        public IApplicationBuilder Configure(IApplicationBuilder app)
        {
            foreach (var module in _modules)
            {
                module.Configure(app);
            }
            return app;
        }
    }
}
