using System.IO;
using System.Text.Json;
using Flurl.Http.Configuration;

namespace Zen.Core.Serializers
{
    public class SystemTextJsonSerialzier : ISerializer
    {
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        public T Deserialize<T>(string s)
            => JsonSerializer.Deserialize<T>(s, options);

        public T Deserialize<T>(Stream stream)
            => JsonSerializer.DeserializeAsync<T>(stream, options).Result;

        public string Serialize(object obj)
            => JsonSerializer.Serialize(obj, options);
    }
}