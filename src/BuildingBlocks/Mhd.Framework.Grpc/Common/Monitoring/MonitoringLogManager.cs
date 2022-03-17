using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Mhd.Framework.Ioc;
using System;
using System.Linq;

namespace Mhd.Framework.Grpc.Common
{
    public class MonitorLogManager : IMonitorLogClient, IMonitorLogServer
    {
        public readonly ILogProvider _logProvider;
        public MonitorLogManager(ILogProvider logProvider) => _logProvider = logProvider;

        public void Dispose() => GC.SuppressFinalize(this);

        public bool TryCreateLog<TRequest>(string actionName, MethodType methodType, TRequest request, bool hasServiceLog, out WebServiceLog log) where TRequest : class
        {
            log = new WebServiceLog
            {
                MonitorLog = new MonitorLog
                {
                    Id = Guid.NewGuid(),
                    ActionName = actionName,
                    ActiveDate = DateTime.Now,
                    MethodType = methodType.ToString()
                },
            };

            if (hasServiceLog)
            {
                var serviceLog = new ServiceLog { Id = Guid.NewGuid(), MonitorLogId = log.MonitorLog.Id, RequestTime = DateTime.Now, };

                serviceLog.Requests.Add(new Request { Id = Guid.NewGuid(), Data = request });

                log.ServiceLog = serviceLog;
            }
            return true;
        }


        #region ServerSide
        public bool TryServerSideAddResponseLog<TResponse>(TResponse response, ServerCallContext contextItems) where TResponse : class
        {
            object hasServiceLogInfo = contextItems.GetHttpContext().Items["HasServiceLog"];

            if (hasServiceLogInfo != null && bool.TryParse(hasServiceLogInfo.ToString(), out bool hasServiceLog))
            {
                if (contextItems.GetHttpContext().Items["SessionInformation"] is WebServiceLog log)
                {
                    if (hasServiceLog)
                    {
                        log.ServiceLog.Responses.Add(new Response { RequestId = log.ServiceLog.Requests.First().Id, Data = response }); //todo:
                        log.ServiceLog.ResponseTime = DateTime.Now;
                        log.ServiceLog.Duration = (long?)DateTime.Now.Subtract(log.ServiceLog.RequestTime).TotalMilliseconds;
                    }
                }

                return true;
            }

            return false;
        }

        public void CompleteServerSideLog(ServerCallContext contextItems)
        {
            if (contextItems.GetHttpContext().Items["SessionInformation"] is WebServiceLog log)
            {
                log.CorrelationId = contextItems.GetCorrelationId();
                _logProvider.WriteLogAsync(log);
            }
        }
        #endregion ServerSide


        #region ClientSide
        public bool TryClientSideAddResponseLog<TResponse>(TResponse response, WebServiceLog log) where TResponse : class
        {
            log.ServiceLog.Responses.Add(new Response { RequestId = log.ServiceLog.Requests.First().Id, Data = response });
            log.ServiceLog.ResponseTime = DateTime.Now;
            log.ServiceLog.Duration = (long?)DateTime.Now.Subtract(log.ServiceLog.RequestTime).TotalMilliseconds;

            return true;
        }

        public void CompleteClientSideLog(WebServiceLog log) => _logProvider.WriteLogAsync(log);
        #endregion ServerSide
    }

    public interface IMonitorLogClient : ITransientDependency
    {
        bool TryClientSideAddResponseLog<TResponse>(TResponse response, WebServiceLog log) where TResponse : class;
        void CompleteClientSideLog(WebServiceLog log);
        bool TryCreateLog<TRequest>(string actionName, MethodType methodType, TRequest request, bool hasServiceLog, out WebServiceLog log) where TRequest : class;
    }
    public interface IMonitorLogServer : IScopedDependency
    {
        bool TryServerSideAddResponseLog<TResponse>(TResponse response, ServerCallContext contextItems) where TResponse : class;
        void CompleteServerSideLog(ServerCallContext contextItems);
        bool TryCreateLog<TRequest>(string actionName, MethodType methodType, TRequest request, bool hasServiceLog, out WebServiceLog log) where TRequest : class;
    }
}
