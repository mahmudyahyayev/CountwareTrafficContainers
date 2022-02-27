//using CountwareTraffic.Services.Devices.Infrastructure;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.IO;

//namespace CountwareTraffic.Services.Devices.DbMigrator
//{
//    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DeviceDbContext>
//    {
//        public DeviceDbContext CreateDbContext(string[] args)
//        {
//            var hostConfig = new ConfigurationBuilder()
//            .SetBasePath(Directory.GetCurrentDirectory())
//            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
//            .AddEnvironmentVariables()
//            .Build();

//            var builder = new DbContextOptionsBuilder<CompanyDbContext>();
//            var connectionString = hostConfig.GetConnectionString("DeviceDbConnection");
//            builder.UseSqlServer(connectionString, x => x.UseNetTopologySuite());


//            return new DeviceDbContext(builder.Options);
//        }
//    }
//}
