using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class ReferencedDocument
    {
        public String DocumentId
        {
            get;
            set;
        }
        public List<ReferencedField> Fields
        {
            get;
            set;
        }
    }
}
