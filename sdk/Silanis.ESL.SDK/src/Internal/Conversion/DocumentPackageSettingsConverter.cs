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
                if (apiSettings.Ceremony.InPerson.HasValue)
                    builder = (apiSettings.Ceremony.InPerson.Value ? builder.WithInPerson() : builder.WithoutInPerson());
                    
                if (apiSettings.Ceremony.OptOutButton.HasValue)
                    builder = (apiSettings.Ceremony.OptOutButton.Value ? builder.WithOptOut() : builder.WithoutOptOut());
                    
                if (apiSettings.Ceremony.DeclineButton.HasValue)
                    builder = (apiSettings.Ceremony.DeclineButton.Value ? builder.WithDecline() : builder.WithoutDecline());
                    
                if (apiSettings.Ceremony.HideWatermark.HasValue)
                    builder = (apiSettings.Ceremony.HideWatermark.Value ? builder.WithoutWatermark() : builder.WithWatermark());
                    
                if (apiSettings.Ceremony.HideCaptureText.HasValue)
                    builder = (apiSettings.Ceremony.HideCaptureText.Value ? builder.WithCaptureText() : builder.WithoutCaptureText());
                    
                if (apiSettings.Ceremony.DisableFirstInPersonAffidavit.HasValue)
                    builder = (apiSettings.Ceremony.DisableFirstInPersonAffidavit.Value ? builder.DisableFirstAffidavit() : builder.EnableFirstAffidavit());
                    
                if (apiSettings.Ceremony.DisableSecondInPersonAffidavit.HasValue)
                    builder = (apiSettings.Ceremony.DisableSecondInPersonAffidavit.Value ? builder.DisableSecondAffidavit() : builder.EnableSecondAffidavit());
                    
                if (apiSettings.Ceremony.HideLanguageDropdown.HasValue)
                    builder = (apiSettings.Ceremony.HideLanguageDropdown.Value ? builder.WithoutLanguageDropDown() : builder.WithLanguageDropDown());
                    
                if (apiSettings.Ceremony.HidePackageOwnerInPerson.HasValue)
                    builder = (apiSettings.Ceremony.HidePackageOwnerInPerson.Value ? builder.HideOwnerInPersonDropDown() : builder.ShowOwnerInPersonDropDown());
            
                foreach (string declineReason in apiSettings.Ceremony.DeclineReasons)
                {
                    builder.WithDeclineReason(declineReason);
                }

                foreach (string optOutReason in apiSettings.Ceremony.OptOutReasons)
                {
                    builder.WithOptOutReason(optOutReason);
                }

                if (apiSettings.Ceremony.MaxAuthFailsAllowed.HasValue)
                    builder.WithMaxAuthAttempts(apiSettings.Ceremony.MaxAuthFailsAllowed.Value);

                if (apiSettings.Ceremony.DocumentToolbarOptions != null && apiSettings.Ceremony.DocumentToolbarOptions.DownloadButton.HasValue)
                {
                    builder = ( apiSettings.Ceremony.DocumentToolbarOptions.DownloadButton.Value ? 
                                    builder.WithDocumentToolbarDownloadButton() : 
                                    builder.WithoutDocumentToolbarDownloadButton() );
                }

                if (apiSettings.Ceremony.Events != null && apiSettings.Ceremony.Events.Complete != null && apiSettings.Ceremony.Events.Complete.Dialog.HasValue)
                {
                    builder = ( apiSettings.Ceremony.Events.Complete.Dialog.Value ?
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

            builder.WithCeremonyLayoutSettings(new CeremonyLayoutSettingsConverter(apiSettings.Ceremony.Layout).ToSDKCeremonyLayoutSettings());

            return builder.Build();
        }
    }
}

