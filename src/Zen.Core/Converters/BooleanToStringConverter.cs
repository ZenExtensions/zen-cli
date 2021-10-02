using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Zen.Core.Converters
{
    public class BooleanToStringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType == JsonTokenType.True)
            {
                return true.ToString();
            }
            else if(reader.TokenType == JsonTokenType.False)
            {
                return false.ToString();
            }
            return reader.GetString();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException("Directly writing object not supported");
        }
    }
}