using System.Collections.Generic;

namespace CountwareTraffic.Services.Events.Core
{
    public record QueryablePagingValue<IEntity>
    {
        public QueryablePagingValue(IList<IEntity> entities, int total)
        {
            Entities = entities;
            Total = total;
        }

        public IList<IEntity> Entities { get; init; }
        public int Total { get; init; }
    }
}
