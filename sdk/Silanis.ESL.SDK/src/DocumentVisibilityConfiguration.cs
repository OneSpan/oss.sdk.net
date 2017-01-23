using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    public class DocumentVisibilityConfiguration
    {
        private List<string> signerIds = new List<string>();

        public string DocumentUid {
            get;
            set;
        }

        public List<string> SignerIds
        {
            get
            {
                return signerIds;
            }
            set
            {
                signerIds = value;
            }
        }

        public void AddSignerIds (IList<string> signerIds)
        {
            this.signerIds.AddRange (signerIds);
        }
    }
}

