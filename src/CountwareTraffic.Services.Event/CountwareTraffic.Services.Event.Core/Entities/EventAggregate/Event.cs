using Mhd.Framework.Efcore;
using System;

namespace CountwareTraffic.Services.Events.Core
{
    public class Event : AggregateRoot, IDeletable  //IAuditable, Not Mahmud Yahyayev Ilgili domain sinifinin Audit loglarinin tutulmasini istemiyorum.Cok fazla inserte tabi tutuluyor. Bilincsiz acmalayalim lutfen
    {
        private string _description;
        private DateTime _eventDate;
        private Guid _deviceId;
        private int _directionTypeId;
        private Guid _createBy;
        private DateTime _createDate;

        public string Description => _description;
        public DateTime EventDate => _eventDate;
        public Guid DeviceId => _deviceId;
        public DirectionType DirectionType { get; private set; }
        public DateTime CreateDate => _createDate;
        public Guid CreatedBy => _createBy;



        #region default properties
        public bool AuditIsDeleted { get; set; }
        #endregion default properties

        private Event() { }

        public static Event Create(string description, DateTime eventDate, Guid deviceId, int directionTypeId, Guid createBy)
        {
            CheckRule(new EventMustHaveDeviceRule(deviceId));

            return new Event
            {
                _eventDate = eventDate,
                _description = description,
                _deviceId = deviceId,
                _directionTypeId = directionTypeId,
                _createBy = createBy,
                _createDate = DateTime.Now
            };
        }

        public void CompleteCreation(string deviceName)
        {
            if (_id == Guid.Empty)
                throw new ArgumentNullException(nameof(_id));

            var drType = DirectionType.From(_directionTypeId);

            AddEvent(new EventCreateCompleted(_deviceId, _id, deviceName, drType, _description, _eventDate, _createDate, _createBy));
        }
    }
}
