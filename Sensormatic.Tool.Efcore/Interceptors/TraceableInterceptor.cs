using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Sensormatic.Tool.Efcore
{
    internal class TraceableInterceptor : InterceptorGenerator<ITraceable>
    {
        private readonly Guid userId;
        public TraceableInterceptor(Guid userId) => this.userId = userId;
        public override void OnAfterError(string execptionMessage) { }
        public override void OnAfterInsert() { }

        public override void OnBeforeDelete(ITraceable item, EntityEntry entityEntry, DbContext dbContext) 
            => item.AuditModifiedBy = userId;

        public override void OnBeforeInsert(ITraceable item, EntityEntry entityEntry, DbContext dbContext) 
            => item.AuditCreateBy = item.AuditModifiedBy = userId;

        public override void OnBeforeUpdate(ITraceable item, EntityEntry entityEntry, DbContext dbContext) 
            => item.AuditModifiedBy = userId;
    }
}
