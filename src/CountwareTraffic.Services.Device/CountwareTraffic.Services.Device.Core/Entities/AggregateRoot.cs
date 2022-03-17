using Mhd.Framework.Efcore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountwareTraffic.Services.Devices.Core
{
    [MetadataType(typeof(IDomainEventRaisable))]
    public abstract class AggregateRoot : Entity<Guid>, IDomainEventRaisable
    {
        [NotMapped]
        public IEnumerable<IDomainEvent> Events { get; set; }

        public AggregateRoot()
        {
            _id = SequentialGuid.Current.Next(null);
            Events = new List<IDomainEvent>();
        }

        public void AddEvent(IDomainEvent @event)
            => ((List<IDomainEvent>)Events).Add(@event);

        public void ClearEvents()
             => ((List<IDomainEvent>)Events).Clear();
    }
}
