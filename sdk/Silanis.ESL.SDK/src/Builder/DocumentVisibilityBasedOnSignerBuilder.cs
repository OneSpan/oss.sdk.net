using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    public class DocumentVisibilityBasedOnSignerBuilder
    {
        private List<DocumentVisibilityConfiguration> configurations = new List<DocumentVisibilityConfiguration>();
        private List<DocumentVisibilityConfigurationBasedOnSignerBuilder> configurationBuilders = new List<DocumentVisibilityConfigurationBasedOnSignerBuilder>();

        public static DocumentVisibilityBasedOnSignerBuilder NewDocumentVisibilityBasedOnSigner() 
        {
            return new DocumentVisibilityBasedOnSignerBuilder();
        }

        public DocumentVisibilityBasedOnSignerBuilder WithConfigurations(List<DocumentVisibilityConfigurationBasedOnSignerBuilder> configurationBuilders) 
        {
            this.configurationBuilders = configurationBuilders;
            return this;
        }

        public DocumentVisibilityBasedOnSignerBuilder AddConfiguration(DocumentVisibilityConfigurationBasedOnSignerBuilder builder) 
        {
            this.configurationBuilders.Add(builder);
            return this;
        }

        public DocumentVisibility Build() 
        {
            DocumentVisibility visibility = new DocumentVisibility();
            visibility.AddConfigurations(ConvertToDocumentVisibilityConfigurations());
            return visibility;
        }

        private List<DocumentVisibilityConfiguration> ConvertToDocumentVisibilityConfigurations() 
        {
            if (configurationBuilders == null || configurationBuilders.Count == 0)
                return new List<DocumentVisibilityConfiguration>();

            foreach (DocumentVisibilityConfigurationBasedOnSignerBuilder builder in configurationBuilders) 
            {
                foreach (string documentId in builder.DocumentIds)
                {
                    MergeConfiguration(DocumentVisibilityConfigurationBuilder.NewDocumentVisibilityConfiguration(documentId).WithSignerId(builder.SignerId).Build());
                }
            }

            return configurations;
        }

        private void MergeConfiguration(DocumentVisibilityConfiguration configuration) 
        {
            DocumentVisibilityConfiguration foundConfiguration = FindSameDocumentIdConfiguration(configuration);
            if (foundConfiguration != null) 
            {
                List<string> signerIds = foundConfiguration.SignerIds;
                foreach(string signerId in configuration.SignerIds) 
                {
                    if(!signerIds.Contains(signerId))
                    {
                        signerIds.Add(signerId);
                    }
                }
            } else 
            {
                configurations.Add(configuration);
            }
        }

        private DocumentVisibilityConfiguration FindSameDocumentIdConfiguration(DocumentVisibilityConfiguration configuration) 
        {
            foreach (DocumentVisibilityConfiguration documentVisibilityConfiguration in configurations) 
            {
                if (string.Equals(documentVisibilityConfiguration.DocumentUid, configuration.DocumentUid))
                {
                    return documentVisibilityConfiguration;
                }
            }
            return null;
        }
    }
}

