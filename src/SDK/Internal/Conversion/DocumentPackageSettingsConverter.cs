using System;
using System.Collections.Generic;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
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
            if ( sdkSettings == null )
                return apiSettings;
                
            CeremonySettings ceremonySettings = new CeremonySettings();

            ceremonySettings.InPerson = sdkSettings.EnableInPerson;
            ceremonySettings.OptOutButton = sdkSettings.EnableOptOut;
            ceremonySettings.DeclineButton = sdkSettings.EnableDecline;
            ceremonySettings.HideWatermark = sdkSettings.HideWatermark;
            ceremonySettings.HideCaptureText = sdkSettings.HideCaptureText;
            ceremonySettings.DeclineReasons = sdkSettings.DeclineReasons;
            ceremonySettings.OptOutReasons = sdkSettings.OptOutReasons;
            ceremonySettings.MaxAuthFailsAllowed = sdkSettings.MaxAuthAttempts;
            ceremonySettings.DisableDeclineOther = sdkSettings.DisableDeclineOther;
            ceremonySettings.DisableOptOutOther = sdkSettings.DisableOptOutOther;
            ceremonySettings.EnforceCaptureSignature = sdkSettings.EnforceCaptureSignature;
            ceremonySettings.Ada = sdkSettings.Ada;
            ceremonySettings.FontSize = sdkSettings.FontSize;
            ceremonySettings.DefaultTimeBasedExpiry = sdkSettings.DefaultTimeBasedExpiry;
            ceremonySettings.RemainingDays = sdkSettings.RemainingDays;
            ceremonySettings.ShowNseHelp = sdkSettings.ShowNseHelp;
            ceremonySettings.LeftMenuExpand = sdkSettings.ExpandLeftMenu;
            ceremonySettings.MaxAttachmentFiles = sdkSettings.MaxAttachmentFiles;
            ceremonySettings.ShowNseOverview = sdkSettings.ShowNseOverview;
            ceremonySettings.ShowNseLogoInIframe = sdkSettings.ShowNseLogoInIframe;

            if (sdkSettings.EnableFirstAffidavit.HasValue) {
                ceremonySettings.DisableFirstInPersonAffidavit = !sdkSettings.EnableFirstAffidavit;
            }

            if (sdkSettings.EnableSecondAffidavit.HasValue) {
                ceremonySettings.DisableSecondInPersonAffidavit = !sdkSettings.EnableSecondAffidavit;
            }

            if (sdkSettings.ShowLanguageDropDown.HasValue) {
                ceremonySettings.HideLanguageDropdown = !sdkSettings.ShowLanguageDropDown;
            }

            if (sdkSettings.ShowOwnerInPersonDropDown.HasValue) {
                ceremonySettings.HidePackageOwnerInPerson = !sdkSettings.ShowOwnerInPersonDropDown;
            }

            if (sdkSettings.LinkHref != null) {
                OneSpanSign.API.Link link = new OneSpanSign.API.Link();
                link.Href =  sdkSettings.LinkHref ;
                link.Text =  sdkSettings.LinkText == null ? sdkSettings.LinkHref : sdkSettings.LinkText ;
                link.Title =  sdkSettings.LinkTooltip == null ? sdkSettings.LinkHref : sdkSettings.LinkTooltip ;
                link.AutoRedirect = sdkSettings.LinkAutoRedirect == null ? false : sdkSettings.LinkAutoRedirect;
                link.Parameters = sdkSettings.LinkParameters;
                ceremonySettings.HandOver = link;
            }

            if ( sdkSettings.ShowDialogOnComplete.HasValue) {
                CeremonyEvents ceremonyEvents = new CeremonyEvents();
                CeremonyEventComplete ceremonyEventComplete = new CeremonyEventComplete();
                ceremonyEventComplete.Dialog = sdkSettings.ShowDialogOnComplete;
                ceremonyEvents.Complete = ceremonyEventComplete;
                ceremonySettings.Events = ceremonyEvents;
            }

            if ( sdkSettings.ShowDownloadButton.HasValue) {
                DocumentToolbarOptions documentToolbarOptions = new DocumentToolbarOptions();
                documentToolbarOptions.DownloadButton = sdkSettings.ShowDownloadButton;
                ceremonySettings.DocumentToolbarOptions = documentToolbarOptions;
            }

            if ( sdkSettings.CeremonyLayoutSettings != null) {
                ceremonySettings.Layout = new CeremonyLayoutSettingsConverter(sdkSettings.CeremonyLayoutSettings).ToAPILayoutOptions();
            }

            IList<OneSpanSign.API.IntegrationFrameworkWorkflow> apiIntegrationFrameworkWorkflows = new List<OneSpanSign.API.IntegrationFrameworkWorkflow>();
            foreach (IntegrationFrameworkWorkflow sdkIfWorkflow in sdkSettings.IntegrationFrameworkWorkflows)
                apiIntegrationFrameworkWorkflows.Add (IntegrationFrameworkWorkflowConverter.ToAPI(sdkIfWorkflow));

            PackageSettings result = new PackageSettings();
            result.Ceremony = ceremonySettings;
            result.IntegrationFrameworkWorkflows = apiIntegrationFrameworkWorkflows;

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

                if (apiSettings.Ceremony.LeftMenuExpand.HasValue)
                    builder = (apiSettings.Ceremony.LeftMenuExpand.Value ? builder.WithLeftMenuExpand() : builder.WithoutLeftMenuExpand());

                if (apiSettings.Ceremony.HideWatermark.HasValue)
                    builder = (apiSettings.Ceremony.HideWatermark.Value ? builder.WithoutWatermark() : builder.WithWatermark());
                    
                if (apiSettings.Ceremony.HideCaptureText.HasValue)
                    builder = (apiSettings.Ceremony.HideCaptureText.Value ? builder.WithoutCaptureText() : builder.WithCaptureText());
                    
                if (apiSettings.Ceremony.DisableFirstInPersonAffidavit.HasValue)
                    builder = (apiSettings.Ceremony.DisableFirstInPersonAffidavit.Value ? builder.DisableFirstAffidavit() : builder.EnableFirstAffidavit());
                    
                if (apiSettings.Ceremony.DisableSecondInPersonAffidavit.HasValue)
                    builder = (apiSettings.Ceremony.DisableSecondInPersonAffidavit.Value ? builder.DisableSecondAffidavit() : builder.EnableSecondAffidavit());

                if (apiSettings.Ceremony.HideLanguageDropdown.HasValue)
                    builder = (apiSettings.Ceremony.HideLanguageDropdown.Value ? builder.WithoutLanguageDropDown() : builder.WithLanguageDropDown());
                    
                if (apiSettings.Ceremony.HidePackageOwnerInPerson.HasValue)
                    builder = (apiSettings.Ceremony.HidePackageOwnerInPerson.Value ? builder.HideOwnerInPersonDropDown() : builder.ShowOwnerInPersonDropDown());

                if (apiSettings.Ceremony.DisableDeclineOther.HasValue)
                    builder = (apiSettings.Ceremony.DisableDeclineOther.Value ? builder.WithoutDeclineOther() : builder.WithDeclineOther());

                if (apiSettings.Ceremony.DisableOptOutOther.HasValue)
                    builder = (apiSettings.Ceremony.DisableOptOutOther.Value ? builder.WithoutOptOutOther() : builder.WithOptOutOther());

                if (apiSettings.Ceremony.EnforceCaptureSignature.HasValue)
                    builder = (apiSettings.Ceremony.EnforceCaptureSignature.Value ? builder.WithEnforceCaptureSignature () : builder.WithoutEnforceCaptureSignature ());

                if (apiSettings.Ceremony.Ada.HasValue)
                    builder = (apiSettings.Ceremony.Ada.Value ? builder.WithAda() : builder.WithoutAda());

                if (apiSettings.Ceremony.FontSize.HasValue)
                    builder.WithFontSize (apiSettings.Ceremony.FontSize.Value);

                if (apiSettings.Ceremony.DefaultTimeBasedExpiry.HasValue)
                    builder = (apiSettings.Ceremony.DefaultTimeBasedExpiry.Value ? builder.WithDefaultTimeBasedExpiry () : builder.WithoutDefaultTimeBasedExpiry ());

                if (apiSettings.Ceremony.RemainingDays.HasValue)
                    builder.WithRemainingDays (apiSettings.Ceremony.RemainingDays.Value);

                if (apiSettings.Ceremony.MaxAttachmentFiles.HasValue)
                    builder.WithMaxAttachmentFiles (apiSettings.Ceremony.MaxAttachmentFiles.Value);

                if (apiSettings.Ceremony.ShowNseHelp.HasValue)
                                    builder = (apiSettings.Ceremony.ShowNseHelp.Value ? builder.WithShowNseHelp () : builder.WithoutShowNseHelp ());

                 if (apiSettings.Ceremony.ShowNseOverview.HasValue)
                                    builder = (apiSettings.Ceremony.ShowNseOverview.Value ? builder.WithShowNseOverview () : builder.WithoutShowNseOverview ());

                if (apiSettings.Ceremony.ShowNseLogoInIframe.HasValue)
                    builder = (apiSettings.Ceremony.ShowNseLogoInIframe.Value ? builder.WithShowNseLogoInIframe()  : builder.WithoutShowNseLogoInIframe() );
 
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
                    builder = (apiSettings.Ceremony.HandOver.AutoRedirect != null && apiSettings.Ceremony.HandOver.AutoRedirect.Value)
                        ? builder.WithHandOverAutoRedirect()
                        : builder.WithoutHandOverAutoRedirect();
                    builder.WithHandOverParameters(apiSettings.Ceremony.HandOver.Parameters);
                }
            }

            builder.WithCeremonyLayoutSettings(new CeremonyLayoutSettingsConverter(apiSettings.Ceremony.Layout).ToSDKCeremonyLayoutSettings());
            foreach (OneSpanSign.API.IntegrationFrameworkWorkflow apiIfWorkflow in apiSettings.IntegrationFrameworkWorkflows)
                builder.WithIfWorkflow(IntegrationFrameworkWorkflowConverter.ToSDK(apiIfWorkflow));

            return builder.Build();
        }
    }
}

