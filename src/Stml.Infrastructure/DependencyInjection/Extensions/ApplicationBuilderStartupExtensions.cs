using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.DependencyInjection.Extensions
{
    public static class ApplicationBuilderStartupExtensions
    {
        public static IApplicationBuilder Configure(this IApplicationBuilder app)
        {
            var moduleContext = app.ApplicationServices.GetService(typeof(IApplicationContext)) as IApplicationContext;
            return moduleContext.Configure(app);
        }
    }
}
