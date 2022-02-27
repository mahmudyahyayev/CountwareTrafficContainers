using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Companies.Application
{
    public class AreaNotFoundException : AppException
    {
        public Guid Id { get; }
        public AreaNotFoundException(Guid id)
            : base(new List<ErrorResult>() { new ErrorResult($"Area with id: {id} was not found") }, 404, ResponseMessageType.Error)
        {
            Id = id;
        }
    }
}
