using System;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
    internal class DocumentVisibilityConfigurationConverter
    {
        private OneSpanSign.API.DocumentVisibilityConfiguration apiConfiguration;
        private DocumentVisibilityConfiguration sdkConfiguration;

        public DocumentVisibilityConfigurationConverter(OneSpanSign.API.DocumentVisibilityConfiguration apiConfiguration) 
        {
            this.apiConfiguration = apiConfiguration;
        }

        public DocumentVisibilityConfigurationConverter(DocumentVisibilityConfiguration sdkConfiguration) 
        {
            this.sdkConfiguration = sdkConfiguration;
        }

        public OneSpanSign.API.DocumentVisibilityConfiguration ToAPIVisibilityConfiguration() 
        {
            if (sdkConfiguration == null) 
            {
                return apiConfiguration;
            }

            OneSpanSign.API.DocumentVisibilityConfiguration configuration = new OneSpanSign.API.DocumentVisibilityConfiguration();
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

