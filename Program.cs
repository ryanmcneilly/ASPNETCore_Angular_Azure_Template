using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ASPNETCore_Angular_Azure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((context, config) =>
                    {
                        var settings = config.Build();

                        // Connect to Azure App Configuration using the Connection String.
                        var appConfigurationConnectionString = settings["CONNECTIONSTRINGS:APPCONFIG"];
                        int appConfigCacheSeconds = System.Int16.Parse(settings["APPCONFIG_CACHESECONDS"]);

                        config.AddAzureAppConfiguration(options =>
                            options
                                .Connect(appConfigurationConnectionString)
                                // Load configuration values with no label
                                .Select(KeyFilter.Any, LabelFilter.Null)
                                .Select(KeyFilter.Any, settings["ENVIRONMENT"])
                                // Override with any configuration values specific to current hosting env
                                //.Select(KeyFilter.Any, hostingContext.HostingEnvironment.EnvironmentName)
                                .ConfigureRefresh((refreshOptions) =>
                                    // indicates that all configuration should be refreshed when the given key has changed.
                                    //refreshOptions.Register(key: "Settings:KeyName", settings["AzureAppConfig:Environment"], refreshAll: true)
                                    refreshOptions.SetCacheExpiration(TimeSpan.FromSeconds(appConfigCacheSeconds))
                                )
                        );

                        settings = config.Build();
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
