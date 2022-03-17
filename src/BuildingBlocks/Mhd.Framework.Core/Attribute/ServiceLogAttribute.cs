using System;

namespace Mhd.Framework.Core
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ServiceLogAttribute : Attribute
    {
    }
}
