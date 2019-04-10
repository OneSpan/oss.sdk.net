using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    public class ConditionalField : Field
    {
        private List<FieldCondition> _conditions = new List<FieldCondition> ();

        public List<FieldCondition> Conditions 
        {
            get 
            {
                return _conditions;
            }
            set 
            {
                _conditions = value;
            }
        }
        public void AddConditions (IList<FieldCondition> conditions)
        {
            _conditions.AddRange (conditions);
        }
    }
}
