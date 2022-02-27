using CountwareTraffic.WorkerServices.Email.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.Email.DbMigrator
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
                        var dbContext = serviceScope.ServiceProvider.GetRequiredService<EmailDbContext>();

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
                    services.AddDbContext<EmailDbContext>(options => options.UseSqlServer(hostContext.Configuration.GetConnectionString("EmailDbConnection")));
                });
    }
}


    /*
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var configuration = GetConfiguration();

            var host = BuildWebHost(configuration, args);

            MigrateDbContext(host, (context, services) =>
            {
                var env = services.GetService<IWebHostEnvironment>();
                var logger = services.GetService<ILogger<EmailDbContextSeed>>();
                new EmailDbContextSeed().SeedAsync(context, env, logger).Wait();
            });

            host.Run();
        }


        static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<EmailDbContext>(options => options.UseSqlServer(hostContext.Configuration.GetConnectionString("EmailDbConnection")));
                })
            .Build();


        static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            return builder;
        }

        static IWebHost MigrateDbContext(IWebHost webHost, Action<EmailDbContext, IServiceProvider> seeder)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<EmailDbContext>>();
                var context = services.GetService<EmailDbContext>();

                try
                {
                    var retries = 10;
                    var retry = Policy.Handle<SqlException>()
                        .WaitAndRetry(
                            retryCount: retries,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                            onRetry: (exception, timeSpan, retry, ctx) =>
                            {
                                logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", nameof(EmailDbContext), exception.GetType().Name, exception.Message, retry, retries);
                            });

                    retry.Execute(() => InvokeSeeder(seeder, context, services));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(EmailDbContext).Name);
                }
            }

            return webHost;
        }
        private static void InvokeSeeder(Action<EmailDbContext, IServiceProvider> seeder, EmailDbContext context, IServiceProvider services)
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
    */
