using System;
using System.Collections.Generic;

namespace Mhd.Framework.Grpc.Common
{
    public class ServiceLog
    {
        public ServiceLog()
        {
            Requests = new List<Request>();
            Responses = new List<Response>();
        }
        public Guid Id { get; set; }
        public object MonitorLogId { get; set; }
        public IList<Request> Requests { get; set; }
        public DateTime RequestTime { get; set; }
        public IList<Response> Responses { get; set; }
        public DateTime ResponseTime { get; set; }
        public long? Duration { get; set; }
    }

    public class Request
    {
        public Guid Id { get; set; }
        public dynamic Data { get; set; }
    }

    public class Response
    {
        public Guid RequestId { get; set; }
        public dynamic Data { get; set; }
    }
}
