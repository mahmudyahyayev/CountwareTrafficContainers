using Convey.CQRS.Commands;
using Sensormatic.Tool.Common;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Application
{
    public class DeviceEventsListenerHandler : ICommandHandler<DeviceEventsListener>
    {
        private readonly IQueueService _queueService;
        public DeviceEventsListenerHandler(IQueueService queueService) => _queueService = queueService;

        public async Task HandleAsync(DeviceEventsListener command)
        {
            string queueName = string.Format(String.Format(Queues.CountwareTrafficEventsDeviceEventsListener, command.DeviceName));

            _queueService.Send(queueName, new Sensormatic.Tool.QueueModel.DeviceEventsListener
            {
                Description = command.Description,
                DeviceId = command.DeviceId,
                DirectionTypeId = command.DirectionTypeId,
                EventDate = command.EventDate,
                RecordId = command.RecordId,
                UserId = command.UserId
            });
        }
    }
}
