using System;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    public class UnknownEnumValuesAsNullConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            Type type = Nullable.GetUnderlyingType(objectType) ?? objectType;
            return type.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
            {
                return null;
            }

            Type enumType = Nullable.GetUnderlyingType(objectType) ?? objectType;
            string enumText = reader.Value.ToString();

            foreach (string name in Enum.GetNames(enumType))
            {
                if (string.Equals(enumText, name, StringComparison.OrdinalIgnoreCase))
                {
                    return Enum.Parse(enumType, name);
                }
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value.ToString());
            }
        }
    }
}
