using System;

namespace Mhd.Framework.Queue
{
    public interface IQueueCommand
    {
        public Guid RecordId
        {
            get; init;
        }
    }
}
