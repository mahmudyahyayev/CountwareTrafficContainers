using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Sensormatic.Tool.Efcore
{
    internal class AuditableInterceptor : InterceptorGenerator<IAuditable>
    {
        private event ContextEntitySaveChangeAuditableHandler _onAuditBeforeDelete;
        private event ContextEntitySaveChangeAuditableHandler _onAuditBeforeInsert;
        private event ContextEntitySaveChangeAuditableHandler _onAuditBeforeUpdate;
        private event ContextEntitySaveChangeAuditableFailedHandler _onAuditAfterError;
        private event ContextEntitySaveChangeAuditableSuccessHandler _onAuditAfterSuccess;

        public AuditableInterceptor(ContextEntitySaveChangeAuditableHandler onAuditBeforeDelete,
                                    ContextEntitySaveChangeAuditableHandler onAuditBeforeInsert,
                                    ContextEntitySaveChangeAuditableHandler onAuditBeforeUpdate,
                                    ContextEntitySaveChangeAuditableSuccessHandler onAuditAfterSuccess,
                                    ContextEntitySaveChangeAuditableFailedHandler onAuditAfterError = default(ContextEntitySaveChangeAuditableFailedHandler))
        {
            _onAuditBeforeDelete = onAuditBeforeDelete;
            _onAuditBeforeInsert = onAuditBeforeInsert;
            _onAuditBeforeUpdate = onAuditBeforeUpdate;
            _onAuditAfterSuccess = onAuditAfterSuccess;
            _onAuditAfterError = onAuditAfterError;
        }

        public override void OnAfterError(string execptionMessage) =>
            _onAuditAfterError?
            .Invoke(execptionMessage);

        public override void OnAfterInsert()
            => _onAuditAfterSuccess?
                .Invoke();


        public override void OnBeforeDelete(IAuditable item, EntityEntry entityEntry, DbContext dbContext) 
            => _onAuditBeforeDelete?
                .Invoke(item, entityEntry, dbContext);

        public override void OnBeforeInsert(IAuditable item, EntityEntry entityEntry, DbContext dbContext)
             => _onAuditBeforeInsert?
                 .Invoke(item, entityEntry, dbContext);

        public override void OnBeforeUpdate(IAuditable item, EntityEntry entityEntry, DbContext dbContext)
            => _onAuditBeforeUpdate?
                .Invoke(item, entityEntry, dbContext);
    }
}
