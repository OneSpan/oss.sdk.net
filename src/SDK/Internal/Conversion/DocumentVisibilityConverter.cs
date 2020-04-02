using System;

namespace OneSpanSign.Sdk
{
    internal class DocumentVisibilityConverter
    {
        private OneSpanSign.API.DocumentVisibility apiVisibility;
        private DocumentVisibility sdkVisibility;

        public DocumentVisibilityConverter(OneSpanSign.API.DocumentVisibility apiVisibility) 
        {
            this.apiVisibility = apiVisibility;
        }

        public DocumentVisibilityConverter(DocumentVisibility sdkVisibility) 
        {
            this.sdkVisibility = sdkVisibility;
        }

        public OneSpanSign.API.DocumentVisibility ToAPIDocumentVisibility() 
        {
            if (sdkVisibility == null) 
            {
                return apiVisibility;
            }

            OneSpanSign.API.DocumentVisibility visibility = new OneSpanSign.API.DocumentVisibility();

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
            
            foreach(OneSpanSign.API.DocumentVisibilityConfiguration configuration in apiVisibility.Configurations) 
            {
                builder.AddConfiguration(new DocumentVisibilityConfigurationConverter(configuration).ToSDKVisibilityConfiguration());
            }

            return builder.Build();
        }
    }
}
