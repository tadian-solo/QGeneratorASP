using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using QGeneratorASP.Data;
using QGeneratorASP.Models;

namespace QGeneratorASP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try { var context = services.GetRequiredService<GQ>();
                    DbInitializer.Initialize(context); }//инициализация бд
                catch (Exception ex)
                {
                    Log.Write(ex);
                }
            }
            host.Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureLogging(logging =>//настраиваем логи
        {
            logging.SetMinimumLevel(LogLevel.Information);
            logging.ClearProviders();
            logging.AddDebug()
            .AddFilter("System", LogLevel.Warning)//фильтры для консольного логгера
               .AddFilter<DebugLoggerProvider>("Microsoft", LogLevel.Warning);

    })
;

    }
}
