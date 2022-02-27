using System;

namespace Sensormatic.Tool.Queue
{
    public interface IQueueUser
    {
        public Guid UserId { get; set; }
    }
}
