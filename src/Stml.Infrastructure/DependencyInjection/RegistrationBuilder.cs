using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.DependencyInjection
{
    public class RegistrationBuilder : IRegistrationBuilder
    {
        public RegistrationBuilder(IServiceCollection services, IEnumerable<Type> typesToRegister)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            TypesToRegister = typesToRegister ?? throw new ArgumentNullException(nameof(typesToRegister));
        }

        public IServiceCollection Services { get; }

        public IEnumerable<Type> TypesToRegister { get; }

        public Func<Type, bool> TypeFilter { get; set; }
    }
}
