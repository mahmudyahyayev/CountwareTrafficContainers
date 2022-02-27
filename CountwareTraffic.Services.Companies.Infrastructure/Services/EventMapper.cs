using CountwareTraffic.Services.Companies.Application;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class EventMapper : IEventMapper
    {
        public IEnumerable<Convey.CQRS.Events.IEvent> MapAll(IEnumerable<Sensormatic.Tool.Queue.IQueueEvent> events)
              => events.Select(Map);

        public Convey.CQRS.Events.IEvent Map(Sensormatic.Tool.Queue.IQueueEvent @event)
        {
            switch (@event)
            {
                case Sensormatic.Tool.QueueModel.SubAreaCreatedCompleted e:
                    return new SubAreaCreatedCompleted
                    {
                        SubAreaId = e.SubAreaId,
                        UserId = e.UserId
                    };

                case Sensormatic.Tool.QueueModel.SubAreaCreatedRejected e:
                    return new SubAreaCreatedRejected
                    {
                        SubAreaId = e.SubAreaId,
                        UserId = e.UserId
                    };

                case Sensormatic.Tool.QueueModel.SubAreaChangedRejected e:
                    return new SubAreaChangedRejected
                    {
                        SubAreaId = e.SubAreaId,
                        Name = e.Name,
                        OldName = e.OldName,
                        UserId = e.UserId
                    };

                case Sensormatic.Tool.QueueModel.SubAreaDeletedRejected e:
                    return new SubAreaDeletedRejected
                    {
                        SubAreaId = e.SubAreaId,
                        UserId = e.UserId
                    };
            }


            return null;
        }
    }
}
