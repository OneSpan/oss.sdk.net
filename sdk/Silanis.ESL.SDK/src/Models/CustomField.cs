using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Silanis.ESL.API
{
    public class CustomField
    {
    
        // Fields
        private IList<Translation> translations = new List<Translation>();

        public CustomField()
        {
        }

        [JsonProperty("id")]
        public String Id
        {
                get; set;
        }

        [JsonProperty("required")]
        public Boolean Required
        {
                    get; set;
        }

        [JsonProperty("value")]
        public String Value
        {
                get; set; 
        }

        [JsonProperty("translations")]
        public IList<Translation> Transalations
        {
            get
            {
                return translations;
            }
        }

        public CustomField AddTransalation(Translation value) 
        {
            if (value == null)
            {
                throw new ArgumentNullException("Argument cannot be null");
            }
            
            translations.Add(value);
            return this;
        }

    }
}

