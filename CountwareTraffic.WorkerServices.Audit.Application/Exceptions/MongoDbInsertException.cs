using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.WorkerServices.Audit.Application
{
    public class MongoDbInsertException : AppException
    {
        public string Message { get; set; }
        public MongoDbInsertException(string message) : base(new List<ErrorResult>() { new ErrorResult(message) }, 500, ResponseMessageType.Error)
        {
            Message = message;
        }
    }
}
