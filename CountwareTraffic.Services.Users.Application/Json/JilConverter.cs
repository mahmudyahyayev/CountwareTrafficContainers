using Jil;
using System;
using System.IO;


namespace CountwareTraffic.Services.Users.Application
{
    public class JilConverter : IJsonConverter
    {
        private static Options _options = new Options(dateFormat: DateTimeFormat.ISO8601, includeInherited: true, excludeNulls: true, serializationNameFormat: SerializationNameFormat.CamelCase);

        public string Serialize<T>(T data)
        {
            using (var output = new StringWriter())
            {
                JSON.Serialize(data, output, _options);
                return output.ToString();
            }
        }

        public T Deserialize<T>(string json)
        {
            return JSON.Deserialize<T>(json, _options);
        }

        public object Deserialize(string json, Type t)
        {
            return JSON.Deserialize(json, t, _options);
        }
    }
}
