using CountwareTraffic.Services.Events.Application;
using CountwareTraffic.Services.Events.Core;
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

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class EventDbContextSeed
    {
        public async Task SeedAsync(EventDbContext context, IWebHostEnvironment env, ILogger<EventDbContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(EventDbContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                var contentRootPath = env.ContentRootPath;

                using (context)
                {
                    context.Database.Migrate();

                    if (!context.DirectionTypes.Any())
                        context.DirectionTypes.AddRange(GetDirectionTypesFromFile(contentRootPath, logger));

                    await context.SaveChangesAsync();
                }
            });
        }


        #region device types
        private IEnumerable<DirectionType> GetDirectionTypesFromFile(string contentRootPath, ILogger<EventDbContextSeed> logger)
        {
            string csvFileDirectionTypes = Path.Combine(contentRootPath, "Setup", "DirectionType.csv");

            if (!File.Exists(csvFileDirectionTypes))
                return GetPredefinedDirectionTypes();

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "DirectionType" };
                csvheaders = GetHeaders(requiredHeaders, csvFileDirectionTypes);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                return GetPredefinedDirectionTypes();
            }

            int id = 1;
            return File.ReadAllLines(csvFileDirectionTypes)
                                        .Skip(1)
                                        .SelectTry(x => CreateDirectionType(x, ref id))
                                        .OnCaughtException(ex => { logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return null; })
                                        .Where(x => x != null);
        }
        private IEnumerable<DirectionType> GetPredefinedDirectionTypes() => Enumeration.GetAll<DirectionType>();
        private DirectionType CreateDirectionType(string value, ref int id)
        {
            if (String.IsNullOrEmpty(value))
                throw new Exception("DirectionType is null or empty");

            return new DirectionType(id++, value.Trim('"').Trim());
        }
        #endregion device types



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
        private AsyncRetryPolicy CreatePolicy(ILogger<EventDbContextSeed> logger, string prefix, int retries = 3)
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
