using System;

namespace Sensormatic.Tool.Core
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ServiceLogAttribute : Attribute
    {
    }
}
