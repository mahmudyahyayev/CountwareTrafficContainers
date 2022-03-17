﻿using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{
    public record DeviceCreated : MessageEnvelope, IQueueEvent
    {
        public Guid DeviceId { get; init; }
        public string Name { get; init; }
        public Guid RecordId { get; init; }
    }
}