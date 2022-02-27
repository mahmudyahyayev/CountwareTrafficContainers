using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Devices.Application
{
    public abstract class AppException : BaseException
    {
        public AppException(ICollection<ErrorResult> errorResults, int statusCode, ResponseMessageType responseMessageType)
            : base(errorResults, statusCode, responseMessageType) { }
    }
}
