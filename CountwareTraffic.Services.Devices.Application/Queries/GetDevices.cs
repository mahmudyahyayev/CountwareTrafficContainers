using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Devices.Application
{
    public class GetDevices : IQuery<PagingResult<DeviceDetailsDto>>
    {
        public Guid SubAreaId { get; set; }
        public PagingQuery PagingQuery { get; set; }
    }
}
