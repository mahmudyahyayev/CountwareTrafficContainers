using CountwareTraffic.Services.Companies.Application;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Companies.Infrastructure.Services
{
    public class QueueEventMapper : IQueueEventMapper
    {
        private readonly ICorrelationService _correlationService;
        public QueueEventMapper(ICorrelationService correlationService)
        {
            _correlationService = correlationService;
        }
        public List<Sensormatic.Tool.Queue.IQueueEvent> MapAll(IEnumerable<Sensormatic.Tool.Efcore.IDomainEvent> events, Guid userId)
             => events.Select(@event => Map(@event, userId)).ToList();

        public Sensormatic.Tool.Queue.IQueueEvent Map(Sensormatic.Tool.Efcore.IDomainEvent @event, Guid userId)
        {
            switch (@event)
            {
                case Core.SubAreaCreated e:
                    return new Sensormatic.Tool.QueueModel.SubAreaCreated()
                    {
                        Name = e.Name,
                        SubAreaId = e.SubAreaId,
                        RecordId = e.RecordId,
                        UserId = userId,
                        CorrelationId = _correlationService.CorrelationId
                    };


                case Core.SubAreaChanged e:
                    return new Sensormatic.Tool.QueueModel.SubAreaChanged()
                    {
                        Name = e.Name,
                        SubAreaId = e.SubAreaId,
                        OldName = e.OldName,
                        RecordId = e.RecordId,
                        UserId = userId,
                        CorrelationId = _correlationService.CorrelationId
                    };


                case Core.SubAreaDeleted e:
                    return new Sensormatic.Tool.QueueModel.SubAreaDeleted()
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
