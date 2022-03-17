using Microsoft.Extensions.Logging;
using Mhd.Framework.Common;
using Mhd.Framework.Grpc.Common;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Api
{
    public class LogProvider : ILogProvider
    {
        private readonly ILogger<LogProvider> _logger;
        public LogProvider(ILogger<LogProvider> logger)
            => _logger = logger;

        public Task WriteLogAsync(WebServiceLog webServiceLog)
        {
            var correlationId = webServiceLog.CorrelationId;

            if (webServiceLog.ServiceLog != null)
            {
                #region requests
                string request = TextJsonExtensions.Serialize(webServiceLog.ServiceLog.Requests);

                if (request.Length > 3200)
                    request = new string(request.Take(3200).ToArray()) + " ...";

                _logger.LogInformation($"Api Request: HasServiceLog-{{HasServiceLog}} {{Action}} {{Request}}  {{ActiveDate}} {{MonitoringId}} {{RequestTime}} {{ResponseTime}} {{ServiceLogId}} {{Correlation_Id}}", true,
                    webServiceLog.MonitorLog.ActionName,
                    request,
                    webServiceLog.MonitorLog.ActiveDate,
                    webServiceLog.MonitorLog.Id,
                    webServiceLog.ServiceLog.RequestTime,
                    webServiceLog.ServiceLog.ResponseTime,
                    webServiceLog.ServiceLog.Id,
                    correlationId);
                #endregion requests




                #region responses
                string response = TextJsonExtensions.Serialize(webServiceLog.ServiceLog.Responses);
                if (response.Length > 3200)
                    response = new string(response.Take(3200).ToArray()) + " ...";

                _logger.LogInformation($"Api Response: {{Body}} {{MonitoringId}} {{ServiceLogId}} {{Correlation_Id}}",
                    response,
                    webServiceLog.MonitorLog.Id,
                    webServiceLog.ServiceLog.Id,
                    correlationId);
                #endregion responses
            }
            else
                _logger.LogInformation($"Api Call: HasServiceLog-{{HasServiceLog}} {{Action}} {{ActiveDate}} {{MonitoringId}} {{Correlation_Id}}",
                    false,
                    webServiceLog.MonitorLog.ActionName,
                    webServiceLog.MonitorLog.ActiveDate, 
                    webServiceLog.MonitorLog.Id,
                    correlationId);

            return Task.CompletedTask;
        }
    }
}
