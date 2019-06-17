using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    public class ReferencedFieldConditions
    {
        public List<FieldCondition> ReferencedInCondition 
        {
            get;
            set;
        }
        public List<FieldCondition> ReferencedInAction
        {
            get;
            set;
        }
    }
}
