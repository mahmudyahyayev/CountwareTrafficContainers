﻿using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.WorkerServices.Audit.Application
{
    public abstract class AppException : BaseException
    {
        public AppException(ICollection<ErrorResult> errorResults, int statusCode, ResponseMessageType responseMessageType)
            : base(errorResults, statusCode, responseMessageType) { }
    }
}
