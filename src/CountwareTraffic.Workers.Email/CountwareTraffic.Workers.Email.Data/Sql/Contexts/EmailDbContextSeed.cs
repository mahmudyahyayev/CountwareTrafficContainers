using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.Email.Data
{
    public class EmailDbContextSeed
    {
        public async Task SeedAsync(EmailDbContext context, IWebHostEnvironment env, ILogger<EmailDbContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(EmailDbContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                var contentRootPath = env.ContentRootPath;

                using (context)
                {
                    context.Database.Migrate();

                    if (!context.EmailTypes.Any())
                        context.EmailTypes.AddRange(GetEmailTypesFromFile(contentRootPath, logger));

                    await context.SaveChangesAsync();
                }
            });
        }


        #region device types
        private IEnumerable<EmailType> GetEmailTypesFromFile(string contentRootPath, ILogger<EmailDbContextSeed> logger)
        {
            string csvFileEmailTypes = Path.Combine(contentRootPath, "Setup", "EmailType.csv");

            if (!File.Exists(csvFileEmailTypes))
                return GetPredefinedEmailTypes();

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "EmailType" };
                csvheaders = GetHeaders(requiredHeaders, csvFileEmailTypes);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                return GetPredefinedEmailTypes();
            }

            int id = 1;
            return File.ReadAllLines(csvFileEmailTypes)
                                        .Skip(1)
                                        .SelectTry(x => CreateEmailType(x, ref id))
                                        .OnCaughtException(ex => { logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return null; })
                                        .Where(x => x != null);
        }
        private IEnumerable<EmailType> GetPredefinedEmailTypes() => Enumeration.GetAll<EmailType>();
        private EmailType CreateEmailType(string value, ref int id)
        {
            if (String.IsNullOrEmpty(value))
                throw new Exception("EmailType is null or empty");

            return new EmailType(id++, value.Trim('"').Trim());
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
        private AsyncRetryPolicy CreatePolicy(ILogger<EmailDbContextSeed> logger, string prefix, int retries = 3)
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
