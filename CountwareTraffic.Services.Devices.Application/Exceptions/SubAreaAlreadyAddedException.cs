using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Devices.Application
{
    public class SubAreaAlreadyAddedException : AppException
    {
        public Guid Id { get; }
        public SubAreaAlreadyAddedException(Guid id)
            : base(new List<ErrorResult>() { new ErrorResult($"SubArea with id: {id} was already added.") }, 409, ResponseMessageType.Error)
        {
            Id = id;
        }
    }
}
