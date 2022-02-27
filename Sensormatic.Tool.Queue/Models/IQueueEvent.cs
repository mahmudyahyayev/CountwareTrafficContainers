using System;

namespace Sensormatic.Tool.Queue
{
    public interface IQueueEvent 
    {
        public Guid RecordId { get; init; }
    }
}
