using CountwareTraffic.Services.Companies.Application;
using CountwareTraffic.Services.Companies.Core;
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

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class AreaDbContextSeed
    {
        public async Task SeedAsync(AreaDbContext context, IWebHostEnvironment env, ILogger<AreaDbContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(AreaDbContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                var contentRootPath = env.ContentRootPath;

                using (context)
                {
                    context.Database.Migrate();

                    if (!context.AreaTypes.Any())
                        context.AreaTypes.AddRange(GetAreaTypesFromFile(contentRootPath, logger));

                    if (!context.SubAreaStatuses.Any())
                        context.SubAreaStatuses.AddRange(GetSubAreaStatusesFromFile(contentRootPath, logger));

                    await context.SaveChangesAsync();
                }
            });
        }


        #region area types
        private IEnumerable<AreaType> GetAreaTypesFromFile(string contentRootPath, ILogger<AreaDbContextSeed> logger)
        {
            string csvFileAreaTypes = Path.Combine(contentRootPath, "Setup", "AreaType.csv");

            if (!File.Exists(csvFileAreaTypes))
                return GetPredefinedAreaTypes();

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "AreaType" };
                csvheaders = GetHeaders(requiredHeaders, csvFileAreaTypes);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                return GetPredefinedAreaTypes();
            }

            int id = 1;
            return File.ReadAllLines(csvFileAreaTypes)
                                        .Skip(1)
                                        .SelectTry(x => CreateCardType(x, ref id))
                                        .OnCaughtException(ex => { logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return null; })
                                        .Where(x => x != null);
        }
        private IEnumerable<AreaType> GetPredefinedAreaTypes() => Enumeration.GetAll<AreaType>();
        private AreaType CreateCardType(string value, ref int id)
        {
            if (String.IsNullOrEmpty(value))
                throw new Exception("AreaType is null or empty");

            return new AreaType(id++, value.Trim('"').Trim());
        }
        #endregion area types


        #region subarea statuses
        private IEnumerable<SubAreaStatus> GetSubAreaStatusesFromFile(string contentRootPath, ILogger<AreaDbContextSeed> loggger)
        {
            string csvFileSubAreaStatuses = Path.Combine(contentRootPath, "Setup", "SubAreaStatus.csv");

            if (!File.Exists(csvFileSubAreaStatuses))
                return GetPredefinedSubAreaStatuses();

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "SubAreaStatus" };
                csvheaders = GetHeaders(requiredHeaders, csvFileSubAreaStatuses);
            }
            catch (Exception ex)
            {
                loggger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                return GetPredefinedSubAreaStatuses();
            }

            int id = 1;
            return File.ReadAllLines(csvFileSubAreaStatuses)
                                        .Skip(1)
                                        .SelectTry(x => CreateSubAreaStatus(x, ref id))
                                        .OnCaughtException(ex => { loggger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return null; })
                                        .Where(x => x != null);
        }
        private IEnumerable<SubAreaStatus> GetPredefinedSubAreaStatuses() => Enumeration.GetAll<SubAreaStatus>();
        private SubAreaStatus CreateSubAreaStatus(string value, ref int id)
        {
            if (String.IsNullOrEmpty(value))
                throw new Exception("SubAreaStatus is null or empty");

            return new SubAreaStatus(id++, value.Trim('"').Trim().ToLowerInvariant());
        }
        #endregion subarea statuses



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
        private AsyncRetryPolicy CreatePolicy(ILogger<AreaDbContextSeed> logger, string prefix, int retries = 3)
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
