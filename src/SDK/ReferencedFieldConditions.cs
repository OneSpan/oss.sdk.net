using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
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
