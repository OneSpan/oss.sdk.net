using System;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
    internal class DocumentPackageSettingsConverter
    {
        private DocumentPackageSettings sdkSettings = null;
        private PackageSettings apiSettings = null;
    
        public DocumentPackageSettingsConverter(DocumentPackageSettings settings)
        {   
            sdkSettings = settings;
        }
        
        public DocumentPackageSettingsConverter(PackageSettings settings)
        {
            apiSettings = settings;
        }
        
        public PackageSettings toAPIPackageSettings()
        {
            if ( apiSettings != null )
                return apiSettings;
                
            PackageSettings result = new PackageSettings();
                
            return result;
        }
        
        public DocumentPackageSettings toSDKDocumentPackageSettings()
        {
            if (sdkSettings != null)
                return sdkSettings;
                
            DocumentPackageSettingsBuilder builder = DocumentPackageSettingsBuilder.NewDocumentPackageSettings();

            if (apiSettings.Ceremony != null)
            {            
                builder = (apiSettings.Ceremony.InPerson ? builder.WithInPerson() : builder.WithoutInPerson());
                builder = (apiSettings.Ceremony.OptOutButton ? builder.WithOptOut() : builder.WithoutOptOut());
                builder = (apiSettings.Ceremony.DeclineButton ? builder.WithDecline() : builder.WithoutDecline());
                builder = (apiSettings.Ceremony.HideWatermark ? builder.WithoutWatermark() : builder.WithWatermark());
                builder = (apiSettings.Ceremony.HideCaptureText ? builder.WithCaptureText() : builder.WithoutCaptureText());
                builder = (apiSettings.Ceremony.DisableFirstInPersonAffidavit ? builder.DisableFirstAffidavit() : builder.EnableFirstAffidavit());
                builder = (apiSettings.Ceremony.DisableSecondInPersonAffidavit ? builder.DisableSecondAffidavit() : builder.EnableSecondAffidavit());
                builder = (apiSettings.Ceremony.HideLanguageDropdown ? builder.WithoutLanguageDropDown() : builder.WithLanguageDropDown());
                builder = (apiSettings.Ceremony.HidePackageOwnerInPerson ? builder.HideOwnerInPersonDropDown() : builder.ShowOwnerInPersonDropDown());
            
                foreach (string reason in apiSettings.Ceremony.OptOutReasons)
                {
                    builder.WithOptOutReason(reason);
                }

                builder.WithMaxAuthAttempts(apiSettings.Ceremony.MaxAuthFailsAllowed);

                if (apiSettings.Ceremony.DocumentToolbarOptions != null)
                {
                    builder = ( apiSettings.Ceremony.DocumentToolbarOptions.DownloadButton ? 
                                    builder.WithDocumentToolbarDownloadButton() : 
                                    builder.WithoutDocumentToolbarDownloadButton() );
                }

                if (apiSettings.Ceremony.Events != null && apiSettings.Ceremony.Events.Complete != null)
                {
                    builder = ( apiSettings.Ceremony.Events.Complete.Dialog ?
                                    builder.WithDialogOnComplete() :
                                    builder.WithoutDialogOnComplete() );
                }

                if (apiSettings.Ceremony.HandOver != null)
                {
                    builder.WithHandOverLinkHref(apiSettings.Ceremony.HandOver.Href);
                    builder.WithHandOverLinkText(apiSettings.Ceremony.HandOver.Text);
                    builder.WithHandOverLinkTooltip(apiSettings.Ceremony.HandOver.Title);
                }
            }
            return builder.Build();
        }
    }
}

