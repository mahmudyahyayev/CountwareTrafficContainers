using Sensormatic.Tool.Efcore;
using System;

namespace CountwareTraffic.Services.Devices.Core
{
    public class Device : AggregateRoot, IAuditable, IDateable, IDeletable, ITraceable
    {
        private string _name;
        private string _description;
        private string _model;
        private Guid _subAreaId;
        private DeviceConnectionInfo _connectionInfo;
        private int _deviceStatusId;
        private int _deviceTypeId;
        private int _deviceCreationStatus;


        public string Name => _name;
        public string Description => _description;
        public string Model => _model;
        public Guid SubAreaId => _subAreaId;
        public DeviceConnectionInfo ConnectionInfo => _connectionInfo;
        public DeviceStatus DeviceStatus { get; private set; }
        public DeviceType DeviceType { get; private set; }
        public DeviceCreationStatus DeviceCreationStatus { get; private set; }


        #region default properties
        public DateTime AuditCreateDate { get; set; }
        public DateTime AuditModifiedDate { get; set; }
        public Guid AuditCreateBy { get; set; }
        public Guid AuditModifiedBy { get; set; }
        public bool AuditIsDeleted { get; set; }
        #endregion
        private Device() { }

        public static Device Create(string name, string description, string model, Guid subAreaId, DeviceConnectionInfo connectionInfo, int deviceTypeId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentNullException(nameof(model));

            if (deviceTypeId < 1)
                throw new InvalidDeviceTypeException(deviceTypeId);

            CheckRule(new DeviceMustHaveSubAreaRule(subAreaId));

            return new Device
            {
                _name = name,
                _description = description,
                _model = model,
                _subAreaId = subAreaId,
                _connectionInfo = connectionInfo,
                _deviceStatusId = DeviceStatus.Unknown.Id,
                _deviceTypeId = deviceTypeId,
                _deviceCreationStatus = DeviceCreationStatus.Created.Id
            };
        }

        public void WhenCreated(Guid deviceId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (deviceId == Guid.Empty)
                throw new ArgumentNullException(nameof(deviceId));

            AddEvent(new DeviceCreated(deviceId, name));
        }

        public void WhenChnaged(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (name != _name)
                AddEvent(new DeviceChanged(_id, name, _name));

            _name = name;
            _description = description;
        }

        public void WhenDeleted(Guid deviceId)
        {
            if (deviceId == Guid.Empty)
                throw new ArgumentNullException(nameof(deviceId));

            AddEvent(new DeviceDeleted(deviceId, _name));
        }

        public void WhenCreatedCompleted() => _deviceCreationStatus = DeviceCreationStatus.Completed.Id;

        public void WhenCreatedRejected() => _deviceCreationStatus = DeviceCreationStatus.Rejected.Id;

        public void WhenChangedRejected(string oldName) => _name = oldName;

        public void WhenDeletedRejected() => AuditIsDeleted = false;
    }
}
