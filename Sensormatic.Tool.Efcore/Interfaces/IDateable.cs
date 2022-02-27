using System;

namespace Sensormatic.Tool.Efcore
{
    public interface IDateable : IInterceptor
    {
        DateTime AuditCreateDate { get; set; }
        DateTime AuditModifiedDate { get; set; }
    }

}
