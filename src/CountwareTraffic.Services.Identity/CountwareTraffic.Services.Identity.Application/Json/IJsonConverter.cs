using System;
using Mhd.Framework.Ioc;
namespace CountwareTraffic.Services.Identity.Application
{
    public interface IJsonConverter : ISingletonDependency
    {
        string Serialize<T>(T data);
        T Deserialize<T>(string json);
        object Deserialize(string json, Type t);
    }
}
