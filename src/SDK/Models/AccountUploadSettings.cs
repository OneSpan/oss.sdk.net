using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
    internal class AccountUploadSettings
    {
        
        [JsonProperty("allowedFileTypes")]
        public List<string> AllowedFileTypes
        {
            get; set;
        }
    }
}