using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    public class DocumentVisibilityConfigurationBasedOnSignerBuilder
    {
        private string signerId;
        private List<string> documentIds = new List<string>();

        public DocumentVisibilityConfigurationBasedOnSignerBuilder(string signerId) 
        {
            this.signerId = signerId;
        }

        public static DocumentVisibilityConfigurationBasedOnSignerBuilder NewDocumentVisibilityConfigurationBasedOnSigner(string signerId) 
        {
            return new DocumentVisibilityConfigurationBasedOnSignerBuilder(signerId);
        }

        public DocumentVisibilityConfigurationBasedOnSignerBuilder WithDocumentIds(List<string> documentIds) 
        {
            this.documentIds.AddRange(documentIds);
            return this;
        }

        public DocumentVisibilityConfigurationBasedOnSignerBuilder WithDocumentId(string documentId) 
        {
            this.documentIds.Add(documentId);
            return this;
        }

        public string SignerId {
            get
            {
                return signerId;
            }
        }

        public List<string> DocumentIds
        {
            get
            {
                return documentIds;
            }
        }
    }
}

