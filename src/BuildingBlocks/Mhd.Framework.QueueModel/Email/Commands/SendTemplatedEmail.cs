using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{
    public record SendTemplatedEmail : IQueueCommand
    {
        public string EmailTemplateAssemblyName { get; set; }
        public dynamic EmailTemplate { get; set; }
        public Guid RecordId { get; init; }
    }
}
