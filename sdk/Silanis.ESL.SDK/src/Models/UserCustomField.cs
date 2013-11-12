using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Silanis.ESL.SDK.API
{
    public class UserCustomField
    {
    
        private string _name=""; // for the purpose of the SchemaSanitizer, so that the backend doesn't throw an exception on null name.

        public UserCustomField()
        {
        }

        [JsonProperty("data")]
        public IDictionary<string, object> Data
        {
            get;
            set;
        }
            
        [JsonProperty("id")]
        public String Id
        {
            get;
            set;
        }
        
        [JsonProperty("name")]
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (Value != null)
                {
                    _name = Value;
                }
            }
        }
        
        [JsonProperty("value")]
        public String Value
        {
            get;
            set;
        }
        
    }
}

