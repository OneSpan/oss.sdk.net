using System;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
    internal class DocumentVisibilityConfigurationConverter
    {
        private Silanis.ESL.API.DocumentVisibilityConfiguration apiConfiguration;
        private DocumentVisibilityConfiguration sdkConfiguration;

        public DocumentVisibilityConfigurationConverter(Silanis.ESL.API.DocumentVisibilityConfiguration apiConfiguration) 
        {
            this.apiConfiguration = apiConfiguration;
        }

        public DocumentVisibilityConfigurationConverter(DocumentVisibilityConfiguration sdkConfiguration) 
        {
            this.sdkConfiguration = sdkConfiguration;
        }

        public Silanis.ESL.API.DocumentVisibilityConfiguration ToAPIVisibilityConfiguration() 
        {
            if (sdkConfiguration == null) 
            {
                return apiConfiguration;
            }

            Silanis.ESL.API.DocumentVisibilityConfiguration configuration = new Silanis.ESL.API.DocumentVisibilityConfiguration();
            configuration.DocumentUid = sdkConfiguration.DocumentUid;
            configuration.RoleUids = sdkConfiguration.SignerIds;
            return configuration;
        }

        public DocumentVisibilityConfiguration ToSDKVisibilityConfiguration() 
        {

            if (apiConfiguration == null) 
            {
                return sdkConfiguration;
            }

            return DocumentVisibilityConfigurationBuilder.NewDocumentVisibilityConfiguration(apiConfiguration.DocumentUid)
                .WithSignerIds(apiConfiguration.RoleUids).Build();
        }
    }
}

