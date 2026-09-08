//
using System;
using Newtonsoft.Json;
namespace OneSpanSign.API
{


    internal class FieldClickableArea
    {

        // Fields

        // Accessors


        [JsonProperty("width")]
        public Nullable<Double> Width
        {
            get; set;
        }


        [JsonProperty("height")]
        public Nullable<Double> Height
        {
            get; set;
        }


        [JsonProperty("alignment")]
        public String Alignment
        {
            get; set;
        }


    }
}
