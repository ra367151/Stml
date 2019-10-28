using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.DependencyInjection
{
    public interface IRegistrationBuilder
    {
        IServiceCollection Services { get; }
        IEnumerable<Type> TypesToRegister { get; }
        Func<Type, bool> TypeFilter { get; set; }
    }
}
