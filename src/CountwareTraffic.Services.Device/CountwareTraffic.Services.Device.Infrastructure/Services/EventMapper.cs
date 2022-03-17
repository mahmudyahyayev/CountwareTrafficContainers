using CountwareTraffic.Services.Devices.Application;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class EventMapper : IEventMapper
    {
        public IEnumerable<Convey.CQRS.Events.IEvent> MapAll(IEnumerable<Mhd.Framework.Queue.IQueueEvent> events)
              => events.Select(Map);

        public Convey.CQRS.Events.IEvent Map(Mhd.Framework.Queue.IQueueEvent @event)
        {
            switch (@event)
            {
                case Mhd.Framework.QueueModel.SubAreaCreated e:
                    return new SubAreaCreated
                    {
                        Name = e.Name,
                        SubAreaId = e.SubAreaId,
                        UserId = e.UserId
                    };


                case Mhd.Framework.QueueModel.SubAreaChanged e:
                    return new SubAreaChanged
                    {
                        OldName = e.OldName,
                        Name = e.Name,
                        SubAreaId = e.SubAreaId,
                        UserId = e.UserId
                    };


                case Mhd.Framework.QueueModel.SubAreaDeleted e:
                    return new SubAreaDeleted
                    {
                        SubAreaId = e.SubAreaId,
                        UserId = e.UserId
                    };


                case Mhd.Framework.QueueModel.DeviceChangedRejected e:
                    return new DeviceChangedRejected
                    {
                        DeviceId = e.DeviceId,
                        Name = e.Name,
                        OldName = e.OldName,
                        UserId = e.UserId
                    };

                case Mhd.Framework.QueueModel.DeviceCreatedCompleted e:
                    return new DeviceCreatedCompleted
                    {
                        DeviceId = e.DeviceId,
                        Name = e.Name,
                        UserId = e.UserId
                    };

                case Mhd.Framework.QueueModel.DeviceCreatedRejected e:
                    return new DeviceCreatedRejected
                    {
                        DeviceId = e.DeviceId,
                        UserId = e.UserId
                    };

                case Mhd.Framework.QueueModel.DeviceDeletedRejected e:
                    return new DeviceDeletedRejected
                    {
                        DeviceId = e.DeviceId,
                        UserId = e.UserId
                    };

                case Mhd.Framework.QueueModel.DeviceStatusChanged e:
                    return new DeviceStatusChanged
                    {
                        DeviceId = e.DeviceId,
                        RecordId = e.RecordId,
                        DeviceStatusId = e.DeviceStatusId
                    };
            }


            return null;
        }
    }
}
