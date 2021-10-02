using System;
using System.Text.Json;
using Zen.Core.Converters;

namespace Zen.Core.Serializers
{
    public static class SerializerExtensions
    {
        internal static readonly Lazy<JsonSerializerOptions> options = new Lazy<JsonSerializerOptions>(() => 
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            options.Converters.Add(new BooleanToStringConverter());
            return options;
        });

        public static TObj Deserialize<TObj>(this string text)
        {
            return JsonSerializer.Deserialize<TObj>(text, options.Value);
        }

        public static string Serialize<TObj>(this TObj obj)
        {
            return JsonSerializer.Serialize(obj, options.Value);
        }
    }
}