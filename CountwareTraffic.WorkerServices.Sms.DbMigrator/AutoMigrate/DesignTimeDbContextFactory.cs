//using CountwareTraffic.Services.Companies.Infrastructure;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.IO;

//namespace CountwareTraffic.WorkerServices.Sms.DbMigrator
//{
//    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CompanyDbContext>
//    {
//        public SmsDbConnection CreateDbContext(string[] args)
//        {
//            var hostConfig = new ConfigurationBuilder()
//            .SetBasePath(Directory.GetCurrentDirectory())
//            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
//            .AddEnvironmentVariables()
//            .Build();

//            var builder = new DbContextOptionsBuilder<SmsDbConnection>();
//            var connectionString = hostConfig.GetConnectionString("SmsDbConnection");
//            builder.UseSqlServer(connectionString, x => x.UseNetTopologySuite());


//            return new SmsDbConnection(builder.Options);
//        }
//    }
//}
