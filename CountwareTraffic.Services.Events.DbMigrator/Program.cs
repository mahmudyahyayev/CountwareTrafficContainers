using CountwareTraffic.Services.Events.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.DbMigrator
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            Console.WriteLine($"Hosting Environment: { Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

            string enteredValue = "";
            do
            {
                Console.WriteLine("Do you want to do the migration process automatically ? (Yes/No)");

                var host = CreateHostBuilder(args).Build();

                enteredValue = Console.ReadLine();

                if (enteredValue.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    using (var serviceScope = host.Services.CreateScope())
                    {
                        var dbContext = serviceScope.ServiceProvider.GetRequiredService<EventDbContext>();

                        Console.WriteLine("Auto migration process started");

                        await dbContext.Database.MigrateAsync();

                        Console.WriteLine("Auto migration process completed successfully.");
                    }

                    await host.RunAsync();

                    break;
                }
                else if (enteredValue.Equals("No", StringComparison.OrdinalIgnoreCase))
                {
                    await host.RunAsync();
                    break;
                }
            }
            while (true);
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(hostConfig =>
                {
                    hostConfig.SetBasePath(Directory.GetCurrentDirectory());
                    hostConfig.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    hostConfig.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);
                    hostConfig.AddEnvironmentVariables();
                    hostConfig.Build();
                })

                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<EventDbContext>(options => options.UseSqlServer(hostContext.Configuration.GetConnectionString("EventDbConnection"), x => x.UseNetTopologySuite()));
                });
    }
}
