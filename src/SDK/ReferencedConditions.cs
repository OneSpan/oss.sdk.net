using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class ReferencedConditions
    {
        public String PackageId
        {
            get;
            set;
        }
        public List<ReferencedDocument> Documents
        {
            get;
            set;
        }
    }
}
