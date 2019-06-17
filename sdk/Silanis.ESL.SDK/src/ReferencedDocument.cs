using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
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
