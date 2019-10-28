﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Stml.Infrastructure.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IRegistrationBuilder RegisterAssemblyTypes(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
                assemblies = new[] { Assembly.GetCallingAssembly() };

            var allPublicTypes = assemblies.SelectMany(
                x => x.GetExportedTypes().Where(y => y.IsClass && !y.IsAbstract && !y.IsNested)
            );

            return new RegistrationBuilder(services, allPublicTypes);
        }

        public static IRegistrationBuilder Where(this IRegistrationBuilder builder, Func<Type, bool> predicate)
        {
            Check.NotNull(builder, nameof(builder));
            builder.TypeFilter = predicate;
            return new RegistrationBuilder(builder.Services, builder.TypesToRegister.Where(predicate));
        }

        public static IServiceCollection AsImplementedInterfaces(this IRegistrationBuilder builder, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            Check.NotNull(builder, nameof(builder));
            foreach (var classType in (builder.TypeFilter == null ? builder.TypesToRegister : builder.TypesToRegister.Where(builder.TypeFilter)))
            {
                var interfaces = classType.GetTypeInfo().ImplementedInterfaces;
                foreach (var infc in interfaces.Where(i => i != typeof(IDisposable) && i.IsPublic && !i.IsNested))
                {
                    builder.Services.Add(new ServiceDescriptor(infc, classType, lifetime));
                }
            }
            return builder.Services;
        }
    }
}
