using System;

namespace Mhd.Framework.Queue
{
    public interface IQueueEvent 
    {
        public Guid RecordId { get; init; }
    }
}
