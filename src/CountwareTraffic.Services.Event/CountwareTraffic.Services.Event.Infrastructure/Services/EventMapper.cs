using CountwareTraffic.Services.Events.Application;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class EventMapper : IEventMapper
    {
        public IEnumerable<Convey.CQRS.Events.IEvent> MapAll(IEnumerable<Mhd.Framework.Queue.IQueueEvent> events)
              => events.Select(Map);

        public Convey.CQRS.Events.IEvent Map(Mhd.Framework.Queue.IQueueEvent @event)
        {
            switch (@event)
            {
                case Mhd.Framework.QueueModel.DeviceCreated e:
                    return new DeviceCreated
                    {
                        Name = e.Name,
                        DeviceId = e.DeviceId,
                        UserId = e.UserId
                    };


                case Mhd.Framework.QueueModel.DeviceChanged e:
                    return new DeviceChanged
                    {
                        Name = e.Name,
                        DeviceId = e.DeviceId,
                        UserId = e.UserId
                    };


                case Mhd.Framework.QueueModel.DeviceDeleted e:
                    return new DeviceDeleted
                    {
                        DeviceId = e.DeviceId,
                        UserId = e.UserId,
                        Name = e.Name
                    };


                case Mhd.Framework.QueueModel.EventCreated e:
                    return new EventCreated
                    {
                        DeviceId = e.DeviceId,
                        EventId = e.EventId,
                        EventDate = e.EventDate,
                        DirectionTypeName = e.DirectionTypeName,
                        DirectionTypeId = e.DirectionTypeId,
                        DeviceName = e.DeviceName,
                        Description = e.Description,
                        CreateBy = e.CreateBy,
                        CreateDate = e.CreateDate,
                        UserId = e.UserId
                    };
            }


            return null;
        }
    }
}
