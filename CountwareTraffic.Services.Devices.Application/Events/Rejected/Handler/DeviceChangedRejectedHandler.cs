﻿using Convey.CQRS.Events;
using CountwareTraffic.Services.Devices.Core;
using Sensormatic.Tool.Queue;
using Sensormatic.Tool.QueueModel;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Application
{
    public class DeviceChangedRejectedHandler : IEventHandler<DeviceChangedRejected>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueService _queueService;
        public DeviceChangedRejectedHandler(IUnitOfWork unitOfWork, IQueueService queueService)
        {
            _unitOfWork = unitOfWork;
            _queueService = queueService;
        }

        public async Task HandleAsync(DeviceChangedRejected command)
        {
            var deviceRepository = _unitOfWork.GetRepository<IDeviceRepository>();

            var device = await deviceRepository.GetAsync(command.DeviceId);

            device.WhenChangedRejected(command.OldName);

            await _unitOfWork.CommitAsync();

            //SignalR ile frontendi besleme kismi
            _queueService.Publish(new DeviceChangedFailed
            {
                RecordId = Guid.NewGuid(),
                DeviceId = command.DeviceId,
                OldName = command.OldName,
                UserId = command.UserId,
                NewName = command.Name,
                UserName = "Test" //todo
            });
        }
    }
}
