using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhd.Framework.Efcore
{
    public interface IDomainEventRaisable : IInterceptor
    {
        [NotMapped]
        IEnumerable<IDomainEvent> Events { get; set; }

        void AddEvent(IDomainEvent @event);
        void ClearEvents();
    }

    public interface IDomainEvent
    {
        public Guid RecordId { get; init; }
    }
}
