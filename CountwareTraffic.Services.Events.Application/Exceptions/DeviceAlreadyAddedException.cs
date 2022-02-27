using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Events.Application
{
    public class DeviceAlreadyAddedException : AppException
    {
        public Guid Id { get; }
        public DeviceAlreadyAddedException(Guid id)
            : base(new List<ErrorResult>() { new ErrorResult($"Device with id: {id} was already added.") }, 409, ResponseMessageType.Error)
        {
            Id = id;
        }
    }
}
