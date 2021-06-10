using System.IO;
using System.Text.Json;
using Flurl.Http.Configuration;

namespace Zen.Core.Serializers
{
    public class SystemTextJsonSerialzier : ISerializer
    {
        public T Deserialize<T>(string s)
            => s.Deserialize<T>();

        public T Deserialize<T>(Stream stream)
            => JsonSerializer.DeserializeAsync<T>(stream, SerializerExtensions.options).Result;

        public string Serialize(object obj)
            => obj.Serialize();
    }
}