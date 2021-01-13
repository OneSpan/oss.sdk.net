using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneSpanSign.API
{
    public class AttachmentFile
    {
        
        [JsonProperty("insertDate"), JsonConverter(typeof(MicrosecondEpochConverter))]
        private DateTime insertDate;

        [JsonProperty ("id")]
        public int Id 
        {
            get; set;
        }

        [JsonProperty ("name")]
        public string Name 
        {
            get; set;
        }

        [JsonProperty ("preview")]
        public bool Preview 
        {
            get; set;
        }

        public DateTime GetInsertDate ()
        {
            return insertDate;
        }


        public void SetInsertDate (long value)
        {
            this.insertDate = new DateTime (value);
        }
    }

    internal class MicrosecondEpochConverter : DateTimeConverterBase
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(((DateTime) value - Epoch).TotalMilliseconds + "");
        }


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            return Epoch.AddMilliseconds((long) reader.Value);
        }
    }
}
