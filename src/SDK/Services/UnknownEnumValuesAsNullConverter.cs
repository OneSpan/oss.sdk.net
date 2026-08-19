using System;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    /// <summary>
    /// Reads an unrecognized enum name as <c>null</c>, so that a value added by a newer server
    /// release does not break deserialization in an older SDK.
    /// <para>
    /// This differs from <see cref="UnknownEnumValuesHandleConverter"/>, which falls back to the
    /// first declared value of the enum. That fallback is unsafe for a status whose first value
    /// means success, because an unrecognized status would be reported as a successful outcome.
    /// </para>
    /// <para>Intended for nullable enum properties only.</para>
    /// </summary>
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
