using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mhd.Framework.Api;
using Mhd.Framework.Common;
using System;
using System.Linq;
using Countware.Traffic.Observability;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator.Controllers
{
    public class BaseController<TController> : BaseApiController where TController : class
    {
        public BaseController(IServiceProvider provider, ILogger<TController> logger, HttpContext context) 
            : base(provider, (webServiceLog, hasServiceLog) =>
            {
                var correlationId = context.GetCorrelationId();

                if (hasServiceLog)
                {
                    string request = TextJsonExtensions.Serialize(webServiceLog.ServiceLog.Request);

                    if (request.Length > 3200)
                        request = new string(request.Take(3200).ToArray()) + " ...";

                    logger.LogInformation($"Api Request: HasServiceLog-{{HasServiceLog}} {{Action}} {{Request}}  {{ActiveDate}} {{MonitoringId}} {{RequestTime}} {{ResponseTime}} {{ServiceLogId}}  {{Correlation_Id}}",
                        hasServiceLog,
                        webServiceLog.MonitorLog.ActionName,
                        request, webServiceLog.MonitorLog.ActiveDate,
                        webServiceLog.MonitorLog.Id,
                        webServiceLog.ServiceLog.RequestTime,
                        webServiceLog.ServiceLog.ResponseTime,
                        webServiceLog.ServiceLog.Id,
                        correlationId);

                    string response = TextJsonExtensions.Serialize(webServiceLog.ServiceLog.Response);
                    if (response.Length > 3200)
                        response = new string(response.Take(3200).ToArray()) + " ...";

                    if (webServiceLog.ServiceLog.IsSuccess)
                        logger.LogInformation($"Api Response: {{Body}} {{MonitoringId}} {{ServiceLogId}} {{Correlation_Id}}",
                            response,
                            webServiceLog.MonitorLog.Id,
                            webServiceLog.ServiceLog.Id,
                            correlationId);

                    else
                        logger.LogError($"{{Error}} {{Correlation_Id}}", response, correlationId);
                }
                else
                    logger.LogInformation($"Api Call: HasServiceLog-{{HasServiceLog}} {{Action}} {{ActiveDate}} {{MonitoringId}} {{Correlation_Id}}",
                        hasServiceLog,
                        webServiceLog.MonitorLog.ActionName,
                        webServiceLog.MonitorLog.ActiveDate,
                        webServiceLog.MonitorLog.Id,
                        correlationId);
            })
        {
            //empty
        }

        protected static new ApiResponse<T> Response<T>(T data, MessageResponse message)
        {
            var response = new ApiResponse<T>
            {
                Data = data,
                Message = message
            };

            return response;
        }

        protected static new ApiResponse<T> Response<T>(T data, ResultMessage message)
        {
            var response = new ApiResponse<T>
            {
                Data = data,
                Message = new MessageResponse
                {
                    CallToActions = message.CallToActionType.HasValue ? CallToActionHelper.Get(message.CallToActionType.Value) : null,
                    Description = message.Description,
                    Title = message.Title
                }
            };

            return response;
        }
    }
}
