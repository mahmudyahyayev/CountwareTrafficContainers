﻿using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Devices.Core
{
    public abstract class DomainException : BaseException
    {
        public DomainException(ICollection<ErrorResult> errorResults, int statusCode, ResponseMessageType responseMessageType)
            : base(errorResults, statusCode, responseMessageType) { }
    }
}
