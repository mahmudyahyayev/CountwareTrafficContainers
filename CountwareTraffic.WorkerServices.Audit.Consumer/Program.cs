using Convey;
using CountwareTraffic.WorkerServices.Audit.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sensormatic.Tool.Queue;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;

namespace CountwareTraffic.WorkerServices.Audit.Consumer
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureHostConfiguration(hostConfig =>
                {

                    var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
                    hostConfig.SetBasePath(Directory.GetCurrentDirectory());
                    hostConfig.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    hostConfig.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
                    hostConfig.AddEnvironmentVariables();
                    hostConfig.Build();
                })

                .UseSerilog((context, loggerConfiguration) =>
                    loggerConfiguration
                        .ReadFrom.Configuration(context.Configuration)
                        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                )
                .ConfigureServices((hostContext, services) =>
                {
                    Sensormatic.Tool.Ioc.IoCGenerator.DoTNet.Current.Start(services, hostContext.Configuration);

                    services.AddConvey()
                            .AddApplication()
                            .Build();

                    services.AddHostedService<Subscriber>();
                    services.AddHostedService<AutoScaler>();
                });
    }
}
