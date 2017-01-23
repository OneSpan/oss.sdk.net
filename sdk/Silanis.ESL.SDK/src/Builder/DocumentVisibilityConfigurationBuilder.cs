using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    public class DocumentVisibilityConfigurationBuilder
    {
        private string documentId;
        private List<string> signerIds = new List<string>();

        public DocumentVisibilityConfigurationBuilder(string documentId)
        {
            this.documentId = documentId;
        }

        public static DocumentVisibilityConfigurationBuilder NewDocumentVisibilityConfiguration(string documentId) 
        {
            return new DocumentVisibilityConfigurationBuilder(documentId);
        }

        public DocumentVisibilityConfigurationBuilder WithSignerIds(List<string> signerIds) 
        {
            this.signerIds.AddRange(signerIds);
            return this;
        }

        public DocumentVisibilityConfigurationBuilder WithSignerId(string signerId) 
        {
            this.signerIds.Add(signerId);
            return this;
        }

        public DocumentVisibilityConfiguration Build() 
        {
            DocumentVisibilityConfiguration configuration = new DocumentVisibilityConfiguration();
            configuration.DocumentUid = documentId;
            configuration.AddSignerIds(signerIds);
            return configuration;
        }
    }
}

