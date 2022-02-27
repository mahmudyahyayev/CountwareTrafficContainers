using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Core
{
    public class DeviceTypeIdNotFoundException : DomainException
    {
        IEnumerable<DeviceType> DeviceTypes { get; }
        public int Id { get; }

        public DeviceTypeIdNotFoundException(IEnumerable<DeviceType> deviceTypes, int id)
            : base(new List<ErrorResult>() { new ErrorResult($"Possible values for DeviceType Id: {String.Join(",", deviceTypes.Select(s => s.Id))}") }, 400, ResponseMessageType.Error)
        {
            DeviceTypes = deviceTypes;
            Id = id;
        }
    }
}
