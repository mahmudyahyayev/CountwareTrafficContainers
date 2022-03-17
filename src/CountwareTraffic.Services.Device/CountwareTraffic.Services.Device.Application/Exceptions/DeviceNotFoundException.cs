using Mhd.Framework.Core;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Devices.Application
{
    public class DeviceNotFoundException : AppException
    {
        public Guid Id { get; }
        public DeviceNotFoundException(Guid id)
            : base(new List<ErrorResult>() { new ErrorResult($"Device with id: {id} was not found") }, 404, ResponseMessageType.Error)
        {
            Id = id;
        }
    }
}
