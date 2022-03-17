using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{
    public record SubAreaCreatedSuccessfully : IQueueEvent
    {
        public Guid SubAreaId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string SubAreaStatus { get; set; }
        public Guid RecordId { get; init; }
    }
}
