using System.Text.Json;

namespace Zen.Core.Serializers
{
    public static class SerializerExtensions
    {
        internal static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public static TObj Deserialize<TObj>(this string text)
        {
            return JsonSerializer.Deserialize<TObj>(text, options);
        }

        public static string Serialize<TObj>(this TObj obj)
        {
            return JsonSerializer.Serialize(obj, options);
        }
    }
}