using System;

namespace Mhd.Framework.Queue
{
    public interface IQueueUser
    {
        public Guid UserId { get; set; }
    }
}
