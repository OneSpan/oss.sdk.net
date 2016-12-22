using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Silanis.ESL.API
{
    internal class DocumentVisibility
    {
        // Fields
        private IList<DocumentVisibilityConfiguration> _configurations = new List<DocumentVisibilityConfiguration>();

        // Accessors
        [JsonProperty("configurations")]
        public IList<DocumentVisibilityConfiguration> Configurations
        {
            get
            {
                return _configurations;
            }
        }
        public DocumentVisibility AddConfiguration(DocumentVisibilityConfiguration value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Argument cannot be null");
            }

            _configurations.Add(value);
            return this;
        }
    }
}

