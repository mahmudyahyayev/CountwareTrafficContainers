using System;
using Sensormatic.Tool.Ioc;
namespace CountwareTraffic.Services.Users.Application
{
    public interface IJsonConverter : ISingletonDependency
    {
        string Serialize<T>(T data);
        T Deserialize<T>(string json);
        object Deserialize(string json, Type t);
    }
}
