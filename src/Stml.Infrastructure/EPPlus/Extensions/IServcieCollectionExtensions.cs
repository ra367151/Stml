using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Stml.Infrastructure.EPPlus.Extensions
{
    public static class IServcieCollectionExtensions
    {
        public static IServiceCollection AddExcelManager(this IServiceCollection services)
        {
            if (!services.Any(x => x.ServiceType == typeof(IHttpClientFactory)))
                services.AddHttpClient();
            services.AddSingleton<IExcelExport, ExcelManager>();
            return services;
        }
    }
}
