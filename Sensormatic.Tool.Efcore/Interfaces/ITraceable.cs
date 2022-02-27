using System;

namespace Sensormatic.Tool.Efcore
{
    public interface ITraceable : IInterceptor
    {
        Guid AuditCreateBy { get; set; }
        Guid AuditModifiedBy { get; set; }
    }
}
