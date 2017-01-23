using System;

namespace Silanis.ESL.SDK
{
    internal class DocumentVisibilityConverter
    {
        private Silanis.ESL.API.DocumentVisibility apiVisibility;
        private DocumentVisibility sdkVisibility;

        public DocumentVisibilityConverter(Silanis.ESL.API.DocumentVisibility apiVisibility) 
        {
            this.apiVisibility = apiVisibility;
        }

        public DocumentVisibilityConverter(DocumentVisibility sdkVisibility) 
        {
            this.sdkVisibility = sdkVisibility;
        }

        public Silanis.ESL.API.DocumentVisibility ToAPIDocumentVisibility() 
        {
            if (sdkVisibility == null) 
            {
                return apiVisibility;
            }

            Silanis.ESL.API.DocumentVisibility visibility = new Silanis.ESL.API.DocumentVisibility();

            foreach(DocumentVisibilityConfiguration configuration in sdkVisibility.Configurations) 
            {
                visibility.AddConfiguration(new DocumentVisibilityConfigurationConverter(configuration).ToAPIVisibilityConfiguration());
            }

            return visibility;
        }

        public DocumentVisibility ToSDKDocumentVisibility() 
        {

            if (apiVisibility == null) 
            {
                return sdkVisibility;
            }

            DocumentVisibilityBuilder builder = DocumentVisibilityBuilder.NewDocumentVisibility();
            
            foreach(Silanis.ESL.API.DocumentVisibilityConfiguration configuration in apiVisibility.Configurations) 
            {
                builder.AddConfiguration(new DocumentVisibilityConfigurationConverter(configuration).ToSDKVisibilityConfiguration());
            }

            return builder.Build();
        }
    }
}
