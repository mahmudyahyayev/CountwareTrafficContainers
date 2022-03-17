using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mhd.Framework.Core;
using Mhd.Framework.Ioc;
using System;
using System.Collections.Generic;

namespace Mhd.Framework.Api
{
    public interface IMonitorLogManager
    {
        bool TryCreate(string actionName, IDictionary<string, object> arguments, bool hasServiceLog, out WebServiceLog log);

        void CompleteLog(ActionExecutedContext context, IDictionary<object, object> contextItems, MonitoringResultHandler OnMonitoringResultHandler);
    }

    public class MonitorLogManager : IMonitorLogManager, IScopedDependency
    {
        public bool TryCreate(string actionName, IDictionary<string, object> arguments, bool hasServiceLog, out WebServiceLog log)
        {
            log = new WebServiceLog
            {
                MonitorLog = new MonitorLog
                {
                    Id = Guid.NewGuid(),
                    ActionName = actionName,
                    ActiveDate = DateTime.Now
                },
            };

            if (hasServiceLog)
            {
                log.ServiceLog = new ServiceLog
                {
                    Id = Guid.NewGuid(),
                    MonitorLogId = log.MonitorLog.Id,
                    Request = arguments,
                    RequestTime = DateTime.Now,
                };
            }

            return true;
        }
        public void CompleteLog(ActionExecutedContext context, IDictionary<object, object> contextItems, MonitoringResultHandler logDispatcher)
        {
            if (bool.TryParse(contextItems["HasServiceLog"]?.ToString(), out bool hasServiceLog))
            {
                if (contextItems["SessionInformation"] is WebServiceLog sessionInformation)
                {
                    object data = null;

                    bool isSuccess = true;

                    if (hasServiceLog)
                    {
                        if (context.Exception != null)
                        {
                            isSuccess = false;
                            data = context.Exception switch
                            {
                                BaseException => ((BaseException)context.Exception).ErrorModel,
                                _ => new ErrorModel(new List<ErrorResult>() { new ErrorResult(context.Exception.Message) }, 500, ResponseMessageType.Error)
                            };
                        }
                        else
                            data = (context.Result as ObjectResult).Value;

                        sessionInformation.ServiceLog.IsSuccess = isSuccess;
                        sessionInformation.ServiceLog.Response = data;
                        sessionInformation.ServiceLog.ResponseTime = DateTime.Now;
                        sessionInformation.ServiceLog.Duration = (long?)DateTime.Now.Subtract(sessionInformation.ServiceLog.RequestTime).TotalMilliseconds;
                    }

                    logDispatcher?.Invoke(sessionInformation, hasServiceLog);
                }
            }
        }

        public void Dispose()
            => GC.SuppressFinalize(this);
    }
}
