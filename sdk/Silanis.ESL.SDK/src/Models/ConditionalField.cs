using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Silanis.ESL.API
{
    internal class ConditionalField : Field
    {
        private IList<FieldCondition> _conditions = new List<FieldCondition> ();

        [JsonProperty ("conditions")]
        public IList<FieldCondition> Conditions 
        {
            get 
            {
                return _conditions;
            }
        }
        public ConditionalField AddCondition (FieldCondition value)
        {
            if (value == null) 
            {
                throw new ArgumentNullException ("Argument cannot be null");
            }

            _conditions.Add (value);
            return this;
        }
    }
}
