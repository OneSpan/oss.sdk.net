using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    public class DocumentVisibility
    {

        private List<DocumentVisibilityConfiguration> configurations = new List<DocumentVisibilityConfiguration>();

        public List<DocumentVisibilityConfiguration> Configurations
        {
            get
            {
                return configurations;
            }
        }

        public void AddConfigurations (IList<DocumentVisibilityConfiguration> configurations)
        {
            this.configurations.AddRange (configurations);
        }

        public List<Document> GetDocuments(DocumentPackage documentPackage, string signerId) 
        {
            List<Document> documents = new List<Document>();
            foreach (Document document in documentPackage.Documents)
            {
                DocumentVisibilityConfiguration configuration = GetConfiguration(document.Id);
                if(configuration != null && configuration.SignerIds.Contains(signerId)) 
                {
                    documents.Add(document);
                }
            }
            return documents;
        }

        public List<Signer> GetSigners(DocumentPackage documentPackage, string documentId) 
        {
            List<Signer> signers = new List<Signer>();
            DocumentVisibilityConfiguration configuration = GetConfiguration(documentId);
            foreach (Signer signer in documentPackage.Signers)
            {
                if(configuration != null && configuration.SignerIds.Contains(signer.Id)) 
                {
                    signers.Add(signer);
                }
            }
            return signers;
        }

        public DocumentVisibilityConfiguration GetConfiguration(string documentUid) 
        {
            foreach (DocumentVisibilityConfiguration configuration in Configurations) 
            {
                if (string.Equals(documentUid, configuration.DocumentUid))
                    return configuration;
            }
            return null;
        }
    }
}

