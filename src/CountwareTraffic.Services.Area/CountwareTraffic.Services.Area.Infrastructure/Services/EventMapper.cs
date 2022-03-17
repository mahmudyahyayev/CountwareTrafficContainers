using CountwareTraffic.Services.Areas.Application;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class EventMapper : IEventMapper
    {
        public IEnumerable<Convey.CQRS.Events.IEvent> MapAll(IEnumerable<Mhd.Framework.Queue.IQueueEvent> events)
              => events.Select(Map);

        public Convey.CQRS.Events.IEvent Map(Mhd.Framework.Queue.IQueueEvent @event)
        {
            switch (@event)
            {
                case Mhd.Framework.QueueModel.SubAreaCreatedCompleted e:
                    return new SubAreaCreatedCompleted
                    {
                        SubAreaId = e.SubAreaId,
                        UserId = e.UserId
                    };

                case Mhd.Framework.QueueModel.SubAreaCreatedRejected e:
                    return new SubAreaCreatedRejected
                    {
                        SubAreaId = e.SubAreaId,
                        UserId = e.UserId
                    };

                case Mhd.Framework.QueueModel.SubAreaChangedRejected e:
                    return new SubAreaChangedRejected
                    {
                        SubAreaId = e.SubAreaId,
                        Name = e.Name,
                        OldName = e.OldName,
                        UserId = e.UserId
                    };

                case Mhd.Framework.QueueModel.SubAreaDeletedRejected e:
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
