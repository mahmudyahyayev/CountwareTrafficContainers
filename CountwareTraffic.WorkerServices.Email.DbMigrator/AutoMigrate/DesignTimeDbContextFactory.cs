//using CountwareTraffic.Services.Companies.Infrastructure;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.IO;

//namespace CountwareTraffic.WorkerServices.Email.DbMigrator
//{
//    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CompanyDbContext>
//    {
//        public EmailDbConnection CreateDbContext(string[] args)
//        {
//            var hostConfig = new ConfigurationBuilder()
//            .SetBasePath(Directory.GetCurrentDirectory())
//            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
//            .AddEnvironmentVariables()
//            .Build();

//            var builder = new DbContextOptionsBuilder<EmailDbConnection>();
//            var connectionString = hostConfig.GetConnectionString("EmailDbConnection");
//            builder.UseSqlServer(connectionString, x => x.UseNetTopologySuite());


//            return new EmailDbConnection(builder.Options);
//        }
//    }
//}
