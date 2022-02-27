using CountwareTraffic.Services.Devices.Application;
using CountwareTraffic.Services.Devices.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class DeviceDbContextSeed
    {
        public async Task SeedAsync(DeviceDbContext context, IWebHostEnvironment env, ILogger<DeviceDbContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(DeviceDbContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                var contentRootPath = env.ContentRootPath;

                using (context)
                {
                    context.Database.Migrate();

                    if (!context.DeviceTypes.Any())
                        context.DeviceTypes.AddRange(GetDeviceTypesFromFile(contentRootPath, logger));

                    if (!context.DeviceStatuses.Any())
                        context.DeviceStatuses.AddRange(GetDeviceStatusesFromFile(contentRootPath, logger));

                    if (!context.DeviceCreationStatuses.Any())
                        context.DeviceCreationStatuses.AddRange(GetDeviceCreationStatusesFromFile(contentRootPath, logger));

                    await context.SaveChangesAsync();
                }
            });
        }


        #region device types
        private IEnumerable<DeviceType> GetDeviceTypesFromFile(string contentRootPath, ILogger<DeviceDbContextSeed> logger)
        {
            string csvFileDeviceTypes = Path.Combine(contentRootPath, "Setup", "DeviceType.csv");

            if (!File.Exists(csvFileDeviceTypes))
                return GetPredefinedDeviceTypes();

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "DeviceType" };
                csvheaders = GetHeaders(requiredHeaders, csvFileDeviceTypes);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                return GetPredefinedDeviceTypes();
            }

            int id = 1;
            return File.ReadAllLines(csvFileDeviceTypes)
                                        .Skip(1)
                                        .SelectTry(x => CreateDeviceType(x, ref id))
                                        .OnCaughtException(ex => { logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return null; })
                                        .Where(x => x != null);
        }
        private IEnumerable<DeviceType> GetPredefinedDeviceTypes() => Enumeration.GetAll<DeviceType>();
        private DeviceType CreateDeviceType(string value, ref int id)
        {
            if (String.IsNullOrEmpty(value))
                throw new Exception("DeviceType is null or empty");

            return new DeviceType(id++, value.Trim('"').Trim());
        }
        #endregion device types



        #region device statuses
        private IEnumerable<DeviceStatus> GetDeviceStatusesFromFile(string contentRootPath, ILogger<DeviceDbContextSeed> loggger)
        {
            string csvFileDeviceStatuses = Path.Combine(contentRootPath, "Setup", "DeviceStatus.csv");

            if (!File.Exists(csvFileDeviceStatuses))
                return GetPredefinedDeviceStatuses();

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "DeviceStatus" };
                csvheaders = GetHeaders(requiredHeaders, csvFileDeviceStatuses);
            }
            catch (Exception ex)
            {
                loggger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                return GetPredefinedDeviceStatuses();
            }

            int id = 1;
            return File.ReadAllLines(csvFileDeviceStatuses)
                                        .Skip(1)
                                        .SelectTry(x => CreateDeviceStatus(x, ref id))
                                        .OnCaughtException(ex => { loggger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return null; })
                                        .Where(x => x != null);
        }
        private IEnumerable<DeviceStatus> GetPredefinedDeviceStatuses() => Enumeration.GetAll<DeviceStatus>();
        private DeviceStatus CreateDeviceStatus(string value, ref int id)
        {
            if (String.IsNullOrEmpty(value))
                throw new Exception("DeviceStatus is null or empty");

            return new DeviceStatus(id++, value.Trim('"').Trim().ToLowerInvariant());
        }
        #endregion device statuses



        #region device statuses
        private IEnumerable<DeviceCreationStatus> GetDeviceCreationStatusesFromFile(string contentRootPath, ILogger<DeviceDbContextSeed> loggger)
        {
            string csvFileDeviceCreationStatus = Path.Combine(contentRootPath, "Setup", "DeviceCreationStatus.csv");

            if (!File.Exists(csvFileDeviceCreationStatus))
                return GetPredefinedDeviceCreationStatuses();

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "DeviceCreationStatus" };
                csvheaders = GetHeaders(requiredHeaders, csvFileDeviceCreationStatus);
            }
            catch (Exception ex)
            {
                loggger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                return GetPredefinedDeviceCreationStatuses();
            }

            int id = 1;
            return File.ReadAllLines(csvFileDeviceCreationStatus)
                                        .Skip(1)
                                        .SelectTry(x => CreateDeviceCreationStatus(x, ref id))
                                        .OnCaughtException(ex => { loggger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return null; })
                                        .Where(x => x != null);
        }
        private IEnumerable<DeviceCreationStatus> GetPredefinedDeviceCreationStatuses() => Enumeration.GetAll<DeviceCreationStatus>();
        private DeviceCreationStatus CreateDeviceCreationStatus(string value, ref int id)
        {
            if (String.IsNullOrEmpty(value))
                throw new Exception("DeviceCreationStatus is null or empty");

            return new DeviceCreationStatus(id++, value.Trim('"').Trim().ToLowerInvariant());
        }
        #endregion device statuses



        #region global
        private string[] GetHeaders(string[] requiredHeaders, string csvfile)
        {
            string[] csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

            if (csvheaders.Count() != requiredHeaders.Count())
                throw new Exception($"requiredHeader count '{ requiredHeaders.Count()}' is different then read header '{csvheaders.Count()}'");

            foreach (var requiredHeader in requiredHeaders)
            {
                if (!csvheaders.Contains(requiredHeader))
                    throw new Exception($"does not contain required header '{requiredHeader}'");
            }

            return csvheaders;
        }
        private AsyncRetryPolicy CreatePolicy(ILogger<DeviceDbContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>()
                                                .WaitAndRetryAsync(
                                                    retryCount: retries,
                                                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                                                    onRetry: (exception, timeSpan, retry, ctx) =>
                                                    {
                                                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                                                    }
                                                );
        }
        #endregion global
    }
}
