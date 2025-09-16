using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    public class SystemAlert
    {
        [JsonProperty ("severityLevel")]
        public SeverityLevel SeverityLevel 
        {
            get; set;
        }
        [JsonProperty ("code")]
        public String Code 
        {
            get; set;
        }
        [JsonProperty ("defaultMessage")]
        public String DefaultMessage 
        {
            get; set;
        }
        [JsonProperty ("parameters")]
        public Dictionary<string,string> Parameters 
        {
            get; set;
        }
    }
    
    public enum SeverityLevel 
    {
        INFO,
        WARNING,
        CRITICAL
    }
}
