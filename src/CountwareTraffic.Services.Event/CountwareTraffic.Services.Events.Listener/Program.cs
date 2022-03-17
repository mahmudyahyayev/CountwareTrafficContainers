using Convey;
using Convey.CQRS.Queries;
using Convey.Docs.Swagger;
using Convey.Types;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Convey.WebApi.Swagger;
using CountwareTraffic.Services.Events.Application;
using CountwareTraffic.Services.Events.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mhd.Framework.Ioc;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Listener
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();
            try
            {
                Log.Information("Starting host...");
                await CreateHostBuilder(args);
            }
            catch (Exception ex) { Log.Fatal(ex, "Host terminated unexpectedly."); }
            finally { Log.CloseAndFlush(); }
        }

        private static async Task CreateHostBuilder(string[] args) =>

              await WebHost.CreateDefaultBuilder(args)
             .UseSerilog()
             .ConfigureServices((hostContext, services) =>
                 {
                     IoCGenerator.DoTNet.Current.Start(services, hostContext.Configuration);

                     services.AddDbContext<EventDbContext>(options =>
                                                 options.UseSqlServer(hostContext.Configuration.GetConnectionString("EventDbConnection"), x => x.UseNetTopologySuite()));

                     services.AddCors(setupAction =>
                     {
                         setupAction.AddPolicy("AllowAll", builder =>
                         {
                             builder.SetIsOriginAllowed((host) => true);
                             builder.AllowAnyMethod();
                             builder.AllowAnyHeader();
                             builder.AllowCredentials();
                         });
                     });

                     services.AddHostedService<AutoCreateDeviceEventEndpointSubscriber>();
                     services.AddHostedService<AutoDeleteDeviceEventEndpointSubscriber>();

                     services.AddSingleton<IEndpointsBuilder, EndpointsBuilder>();

                     services.AddConvey()
                        .AddWebApi()
                        .AddWebApiSwaggerDocs()
                        .AddApplication()
                        .Build();
                 })
             .Configure(app =>
              {
                  var _queryDispatcher = app.ApplicationServices.GetRequiredService<IQueryDispatcher>();

                  var devices = _queryDispatcher.QueryAsync(new GetDevices { }).Result;

                  app
                    .UseSwaggerDocs()
                    .UseCors("AllowAll")
                    .UsePublicContracts<ContractAttribute>()
                    .UseDispatcherEndpoints(endpoints =>
                    {
                        endpoints.Get("", ctx => ctx.Response.WriteAsync(ctx.RequestServices.GetService<AppOptions>().Name));

                        foreach (var device in devices)
                        {
                            endpoints.Post<EventLog>($"api/v1/events/{device.Name}",
                                beforeDispatch: (eventLog, ctx) =>
                                 {
                                     eventLog.DeviceId = device.Id;
                                     eventLog.DeviceName = device.Name;
                                     return Task.CompletedTask;
                                 },
                                afterDispatch: (cmd, ctx) =>
                                {
                                    return ctx.Response.Created();
                                });
                        }
                    });
              })

              .Build()
              .RunAsync();
    }

}

