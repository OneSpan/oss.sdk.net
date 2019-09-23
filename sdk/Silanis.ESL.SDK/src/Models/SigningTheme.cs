using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Silanis.ESL.API
{

    internal class SigningTheme
    {

        // Fields
        private Dictionary<string, string> _color = new Dictionary<string, string> ();

        // Accessors

        [JsonProperty ("color")]
        public Dictionary<string, string> Color {
            get; set;
        }
    }
}