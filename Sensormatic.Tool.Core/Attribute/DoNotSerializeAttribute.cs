using System;

namespace Sensormatic.Tool.Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DoNotSerializeAttribute : Attribute
    {
    }
}
