﻿using Sensormatic.Tool.Queue;
using System;

namespace Sensormatic.Tool.QueueModel
{
    public record DeviceCreated : MessageEnvelope, IQueueEvent
    {
        public Guid DeviceId { get; init; }
        public string Name { get; init; }
        public Guid RecordId { get; init; }
    }
}
