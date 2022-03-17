using CountwareTraffic.Services.Areas.Application;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class QueueEventMapper : IQueueEventMapper
    {
        private readonly ICorrelationService _correlationService;
        public QueueEventMapper(ICorrelationService correlationService)
        {
            _correlationService = correlationService;
        }
        public List<Mhd.Framework.Queue.IQueueEvent> MapAll(IEnumerable<Mhd.Framework.Efcore.IDomainEvent> events, Guid userId)
             => events.Select(@event => Map(@event, userId)).ToList();

        public Mhd.Framework.Queue.IQueueEvent Map(Mhd.Framework.Efcore.IDomainEvent @event, Guid userId)
        {
            switch (@event)
            {
                case Core.SubAreaCreated e:
                    return new Mhd.Framework.QueueModel.SubAreaCreated()
                    {
                        Name = e.Name,
                        SubAreaId = e.SubAreaId,
                        RecordId = e.RecordId,
                        UserId = userId,
                        CorrelationId = _correlationService.CorrelationId
                    };


                case Core.SubAreaChanged e:
                    return new Mhd.Framework.QueueModel.SubAreaChanged()
                    {
                        Name = e.Name,
                        SubAreaId = e.SubAreaId,
                        OldName = e.OldName,
                        RecordId = e.RecordId,
                        UserId = userId,
                        CorrelationId = _correlationService.CorrelationId
                    };


                case Core.SubAreaDeleted e:
                    return new Mhd.Framework.QueueModel.SubAreaDeleted()
                    {
                        SubAreaId = e.SubAreaId,
                        RecordId = e.RecordId,
                        UserId = userId,
                        CorrelationId = _correlationService.CorrelationId
                    };

                case Core.AreaTypeChanged e:
                    return null;
            }

            return null;
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
