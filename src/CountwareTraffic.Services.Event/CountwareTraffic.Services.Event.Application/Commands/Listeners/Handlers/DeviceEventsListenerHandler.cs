using Convey.CQRS.Commands;
using Mhd.Framework.Common;
using Mhd.Framework.Queue;
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

            _queueService.Send(queueName, new Mhd.Framework.QueueModel.DeviceEventsListener
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
