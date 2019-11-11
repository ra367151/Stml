using System;
using Microsoft.AspNetCore.Hosting;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Stml.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Stml.Web.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                Migrate<StmlDbContext>(serviceProvider);
                SeedDatas(serviceProvider);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging(loggingBuilder => loggingBuilder.AddNLog());
                    webBuilder.UseStartup<Startup>();
                });

        private static void Migrate<TDbContext>(IServiceProvider serviceProvider) where TDbContext : DbContext
        {
            var context = serviceProvider.GetService<TDbContext>();
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occured while migrate DbContext: {0}", typeof(TDbContext).Name);
                throw;
            }
        }

        private static void SeedDatas(IServiceProvider serviceProvider)
        {
            try
            {
                DataSeeder.Seed(serviceProvider);
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while [{0}] seeding datas.", nameof(DataSeeder));
                throw;
            }
        }
    }
}
