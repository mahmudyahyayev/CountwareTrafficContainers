using System;

namespace Sensormatic.Tool.Queue
{
    public interface IQueueCommand
    {
        public Guid RecordId
        {
            get; init;
        }
    }
}
