using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Server.HttpSys;

namespace LJH.RegionMonitor.WebAPI
{
    public class WebHostStarter
    {
        public static void StartWebListenerHost(string url, string dbConnect)
        {
            WebHost.CreateDefaultBuilder()
                   .UseContentRoot(Directory.GetCurrentDirectory())
                   .UseHttpSys(options =>
                   {
                       options.Authentication.Schemes = AuthenticationSchemes.None;
                       options.Authentication.AllowAnonymous = true;
                       options.MaxConnections = 100;
                       options.MaxRequestBodySize = 30000000;
                       options.UrlPrefixes.Add(url);
                   })
                    .ConfigureAppConfiguration((hostContext, config) =>
                    {
                        var env = hostContext.HostingEnvironment;
                        config.SetBasePath(Directory.GetCurrentDirectory());
                        config.AddJsonFile("appsettings.json", true, true);
                        config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
                        config.AddInMemoryCollection(new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("DBConnect", dbConnect) });
                    })
                   .ConfigureLogging(lb =>
                   {
                       lb.AddLog4Net();
                       lb.AddFilter<Log4NetProvider>("Microsoft.EntityFrameworkCore", LogLevel.Warning);
                       lb.AddFilter<Log4NetProvider>("Microsoft", LogLevel.Information);
                       lb.AddFilter<Log4NetProvider>("System", LogLevel.Information);
                   })
                   .UseStartup<Startup>()
                   .Build()
                   .Start();
        }
    }
}