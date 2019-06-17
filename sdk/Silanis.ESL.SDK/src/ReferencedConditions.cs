using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
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
