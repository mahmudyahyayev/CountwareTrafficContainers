using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Sensormatic.Tool.Efcore
{
    public interface IInterceptorGenerator
    {
        void OnBefore(EntityEntry item, DbContext objectContext);
        void OnAfter();
        void OnError(Exception exception);
    }
}
