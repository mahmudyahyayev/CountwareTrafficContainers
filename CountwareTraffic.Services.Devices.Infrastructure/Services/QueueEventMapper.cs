using CountwareTraffic.Services.Devices.Application;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class QueueEventMapper : IQueueEventMapper
    {
        private readonly ICorrelationService _correlationService;
        public QueueEventMapper(ICorrelationService correlationService)
        {
            _correlationService = correlationService;
        }

        public List<Sensormatic.Tool.Queue.IQueueEvent> MapAll(IEnumerable<Sensormatic.Tool.Efcore.IDomainEvent> events, Guid userId)
             => events.Select(e => Map(e, userId)).ToList();

        public Sensormatic.Tool.Queue.IQueueEvent Map(Sensormatic.Tool.Efcore.IDomainEvent @event, Guid userId)
        {
            switch (@event)
            {
                case Core.DeviceCreated e:
                    return new Sensormatic.Tool.QueueModel.DeviceCreated()
                    {
                        Name = e.Name,
                        DeviceId = e.DeviceId,
                        RecordId = e.RecordId,
                        UserId = userId,
                        CorrelationId = _correlationService.CorrelationId
                    };


                case Core.DeviceChanged e:
                    return new Sensormatic.Tool.QueueModel.DeviceChanged()
                    {
                        Name = e.Name,
                        DeviceId = e.DeviceId,
                        OldName = e.OldName,
                        RecordId = e.RecordId,
                        UserId = userId,
                        CorrelationId = _correlationService.CorrelationId
                    };


                case Core.DeviceDeleted e:
                    return new Sensormatic.Tool.QueueModel.DeviceDeleted()
                    {
                        DeviceId = e.DeviceId,
                        RecordId = e.RecordId,
                        UserId = userId,
                        Name = e.Name,
                        CorrelationId = _correlationService.CorrelationId
                    };
            }

            return null;
        }
        public void Dispose() => GC.SuppressFinalize(this);
    }
}
