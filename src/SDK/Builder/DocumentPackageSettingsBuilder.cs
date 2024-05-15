using System;
using System.Collections.Generic;
using OneSpanSign.Sdk.Internal;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
    public class DocumentPackageSettingsBuilder
    {
        private Nullable<bool> enableInPerson = null;
        private Nullable<bool> enableOptOut = null;
        private Nullable<bool> enableDecline = null;
        private Nullable<bool> expandLeftMenu = null;
        private Nullable<bool> hideWatermark = null;
        private Nullable<bool> hideCaptureText = null;
        private List<string> declineReasons = new List<string> ();
        private List<string> optOutReasons = new List<string> ();
        private Nullable<int> maxAuthAttempts = null;
        private Nullable<bool> showDocumentToolbarDownloadButton = null;
        private Nullable<bool> showDialogOnComplete = null;
        private Nullable<bool> showLanguageDropDown = null;
        private Nullable<bool> enableFirstAffidavit = null;
        private Nullable<bool> enableSecondAffidavit = null;
        private Nullable<bool> showOwnerInPersonDropDown = null;
        private Nullable<bool> disableDeclineOther = null;
        private Nullable<bool> disableOptOutOther = null;
        private Nullable<bool> enforceCaptureSignature = null;
        private Nullable<bool> ada = null;
        private Nullable<Int32> fontSize = null;
        private Nullable<bool> defaultTimeBasedExpiry = null;
        private Nullable<Int32> remainingDays = null;
        private Nullable<Int32> maxAttachmentFiles = null;
        private Nullable<bool> showNseHelp = null;
        private Nullable<bool> showNseOverview = null;
        private Nullable<bool> showNseLogoInIframe = null;

        private string linkText = null;
        private string linkTooltip = null;
        private string linkHref = null;
        private Nullable<bool> linkAutoRedirect = null;
        private HashSet<String> linkParameters = null;

        private CeremonyLayoutSettings ceremonyLayoutSettings = null;
        private List<IntegrationFrameworkWorkflow> integrationFrameworkWorkflows = new List<IntegrationFrameworkWorkflow> ();

        private DocumentPackageSettingsBuilder ()
        {
        }

        public DocumentPackageSettingsBuilder ShowOwnerInPersonDropDown ()
        {
            showOwnerInPersonDropDown = true;
            return this;
        }

        public DocumentPackageSettingsBuilder HideOwnerInPersonDropDown ()
        {
            showOwnerInPersonDropDown = false;
            return this;
        }

        public DocumentPackageSettingsBuilder EnableFirstAffidavit ()
        {
            enableFirstAffidavit = true;
            return this;
        }

        public DocumentPackageSettingsBuilder DisableFirstAffidavit ()
        {
            enableFirstAffidavit = false;
            return this;
        }

        public DocumentPackageSettingsBuilder EnableSecondAffidavit ()
        {
            enableSecondAffidavit = true;
            return this;
        }

        public DocumentPackageSettingsBuilder DisableSecondAffidavit ()
        {
            enableSecondAffidavit = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithLanguageDropDown ()
        {
            showLanguageDropDown = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutLanguageDropDown ()
        {
            showLanguageDropDown = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutDocumentToolbarDownloadButton ()
        {
            showDocumentToolbarDownloadButton = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithDocumentToolbarDownloadButton ()
        {
            showDocumentToolbarDownloadButton = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithDialogOnComplete ()
        {
            showDialogOnComplete = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutDialogOnComplete ()
        {
            showDialogOnComplete = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithInPerson ()
        {
            enableInPerson = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutInPerson ()
        {
            enableInPerson = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithOptOut ()
        {
            enableOptOut = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutOptOut ()
        {
            enableOptOut = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithDecline ()
        {
            enableDecline = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutDecline ()
        {
            enableDecline = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithLeftMenuExpand ()
        {
            expandLeftMenu = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutLeftMenuExpand ()
        {
            expandLeftMenu = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithWatermark ()
        {
            hideWatermark = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutWatermark ()
        {
            hideWatermark = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithAda ()
        {
            ada = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutAda ()
        {
            ada = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithFontSize (Nullable<Int32> fontSize)
        {
            this.fontSize = fontSize;
            return this;
        }

        public DocumentPackageSettingsBuilder WithDefaultTimeBasedExpiry ()
        {
            defaultTimeBasedExpiry = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutDefaultTimeBasedExpiry ()
        {
            defaultTimeBasedExpiry = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithRemainingDays (Nullable<Int32> remainingDays)
        {
            this.remainingDays = remainingDays;
            return this;
        }

        public DocumentPackageSettingsBuilder WithShowNseHelp ()
        {
            showNseHelp = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutShowNseHelp ()
        {
            showNseHelp = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithShowNseOverview ()
        {
            showNseOverview = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutShowNseOverview ()
        {
            showNseOverview = false;
            return this;
        }
        
        public DocumentPackageSettingsBuilder WithShowNseLogoInIframe ()
        {
            showNseLogoInIframe = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutShowNseLogoInIframe ()
        {
            showNseLogoInIframe = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithCaptureText ()
        {
            hideCaptureText = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutCaptureText ()
        {
            hideCaptureText = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithHandOverLinkHref (String href)
        {
            Asserts.NotEmptyOrNull (href, "href");

            linkHref = href;

            //If no protocol was specified, we assume https
            if (!linkHref.StartsWith ("http://") && !linkHref.StartsWith ("https://")) {
                linkHref = "https://" + linkHref;
            }

            return this;
        }

        public DocumentPackageSettingsBuilder WithHandOverLinkText (String text)
        {
            linkText = text;
            return this;
        }

        public DocumentPackageSettingsBuilder WithHandOverLinkTooltip (String tooltip)
        {
            linkTooltip = tooltip;
            return this;
        }
        
        public DocumentPackageSettingsBuilder WithHandOverAutoRedirect()
        {
            this.linkAutoRedirect = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutHandOverAutoRedirect ()
        {
            this.linkAutoRedirect = false;
            return this;
        }
        
        public DocumentPackageSettingsBuilder WithHandOverParameters (HashSet<String> parameters)
        {
            linkParameters = parameters;
            return this;
        }

        public DocumentPackageSettingsBuilder WithDeclineReason (String reason)
        {
            declineReasons.Add (reason);
            return this;
        }

        public DocumentPackageSettingsBuilder WithOptOutReason (String reason)
        {
            optOutReasons.Add (reason);
            return this;
        }

        public DocumentPackageSettingsBuilder WithCeremonyLayoutSettings (CeremonyLayoutSettingsBuilder builder)
        {
            return WithCeremonyLayoutSettings (builder.Build ());
        }

        public DocumentPackageSettingsBuilder WithCeremonyLayoutSettings (CeremonyLayoutSettings ceremonyLayoutSettings)
        {
            this.ceremonyLayoutSettings = ceremonyLayoutSettings;
            return this;
        }

        public DocumentPackageSettingsBuilder WithMaxAuthAttempts (int maxAuthAttempts)
        {
            this.maxAuthAttempts = maxAuthAttempts;
            return this;
        }

        public DocumentPackageSettingsBuilder WithDeclineOther ()
        {
            this.disableDeclineOther = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutDeclineOther ()
        {
            this.disableDeclineOther = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithOptOutOther ()
        {
            this.disableOptOutOther = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutOptOutOther ()
        {
            this.disableOptOutOther = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithEnforceCaptureSignature ()
        {
            this.enforceCaptureSignature = true;
            return this;
        }

        public DocumentPackageSettingsBuilder WithoutEnforceCaptureSignature ()
        {
            this.enforceCaptureSignature = false;
            return this;
        }

        public DocumentPackageSettingsBuilder WithMaxAttachmentFiles (Nullable<Int32> maxAttachmentFiles)
        {
            this.maxAttachmentFiles = maxAttachmentFiles;
            return this;
        }

        public DocumentPackageSettingsBuilder WithIfWorkflow (IntegrationFrameworkWorkflow ifWorkflow)
        {
            integrationFrameworkWorkflows.Add (ifWorkflow);
            return this;
        }

        public DocumentPackageSettings build ()
        {
            return Build ();
        }

        public DocumentPackageSettings Build ()
        {
            DocumentPackageSettings result = new DocumentPackageSettings ();

            result.EnableInPerson = enableInPerson;
            result.EnableOptOut = enableOptOut;
            result.EnableDecline = enableDecline;
            result.ExpandLeftMenu = expandLeftMenu;
            result.HideWatermark = hideWatermark;
            result.HideCaptureText = hideCaptureText;
            foreach (string declineReason in declineReasons)
                result.DeclineReasons.Add (declineReason);
            foreach (string optOutReason in optOutReasons)
                result.OptOutReasons.Add (optOutReason);
            result.MaxAuthAttempts = maxAuthAttempts;
            result.ShowDownloadButton = showDocumentToolbarDownloadButton;
            result.ShowDialogOnComplete = showDialogOnComplete;
            result.ShowLanguageDropDown = showLanguageDropDown;
            result.EnableFirstAffidavit = enableFirstAffidavit;
            result.EnableSecondAffidavit = enableSecondAffidavit;
            result.ShowOwnerInPersonDropDown = showOwnerInPersonDropDown;
            result.DisableDeclineOther = disableDeclineOther;
            result.DisableOptOutOther = disableOptOutOther;
            result.EnforceCaptureSignature = enforceCaptureSignature;
            result.Ada = ada;
            result.FontSize = fontSize;
            result.DefaultTimeBasedExpiry = defaultTimeBasedExpiry;
            result.RemainingDays = remainingDays;
            result.ShowNseHelp = showNseHelp;
            result.LinkHref = linkHref;
            result.LinkText = linkText;
            result.LinkTooltip = linkTooltip;
            result.LinkAutoRedirect = linkAutoRedirect;
            result.LinkParameters = linkParameters;
            result.MaxAttachmentFiles = maxAttachmentFiles;
            result.ShowNseOverview = showNseOverview;
            result.ShowNseLogoInIframe = showNseLogoInIframe;

            result.CeremonyLayoutSettings = ceremonyLayoutSettings;
            foreach (IntegrationFrameworkWorkflow ifWorkflow in integrationFrameworkWorkflows)
                result.IntegrationFrameworkWorkflows.Add (ifWorkflow);

            return result;
        }

        public static DocumentPackageSettingsBuilder NewDocumentPackageSettings ()
        {
            return new DocumentPackageSettingsBuilder ();
        }

        internal DocumentPackageSettingsBuilder (PackageSettings apiPackageSettings)
        {
            enableInPerson = apiPackageSettings.Ceremony.InPerson;
            enableOptOut = apiPackageSettings.Ceremony.OptOutButton;
            enableDecline = apiPackageSettings.Ceremony.DeclineButton;
            hideWatermark = apiPackageSettings.Ceremony.HideWatermark;
            hideCaptureText = apiPackageSettings.Ceremony.HideCaptureText;
            enableFirstAffidavit = !apiPackageSettings.Ceremony.DisableFirstInPersonAffidavit;
            enableSecondAffidavit = !apiPackageSettings.Ceremony.DisableSecondInPersonAffidavit;
            showOwnerInPersonDropDown = !apiPackageSettings.Ceremony.HidePackageOwnerInPerson;
            showLanguageDropDown = !apiPackageSettings.Ceremony.HideLanguageDropdown;
            disableDeclineOther = apiPackageSettings.Ceremony.DisableDeclineOther;
            disableOptOutOther = apiPackageSettings.Ceremony.DisableOptOutOther;
            enforceCaptureSignature = apiPackageSettings.Ceremony.EnforceCaptureSignature;
            ada = apiPackageSettings.Ceremony.Ada;


            foreach (string declineReason in apiPackageSettings.Ceremony.DeclineReasons) {
                declineReasons.Add (declineReason);
            }

            foreach (string optOutReason in apiPackageSettings.Ceremony.OptOutReasons) {
                optOutReasons.Add (optOutReason);
            }

            maxAuthAttempts = apiPackageSettings.Ceremony.MaxAuthFailsAllowed;
            maxAttachmentFiles = apiPackageSettings.Ceremony.MaxAttachmentFiles;

            if (apiPackageSettings.Ceremony.DocumentToolbarOptions != null) {
                showDocumentToolbarDownloadButton = apiPackageSettings.Ceremony.DocumentToolbarOptions.DownloadButton;
            }

            if (apiPackageSettings.Ceremony.Events != null && apiPackageSettings.Ceremony.Events.Complete != null) {
                showDialogOnComplete = apiPackageSettings.Ceremony.Events.Complete.Dialog;
            }

            if (apiPackageSettings.Ceremony.HandOver != null) {
                linkHref = apiPackageSettings.Ceremony.HandOver.Href;
                linkText = apiPackageSettings.Ceremony.HandOver.Text;
                linkTooltip = apiPackageSettings.Ceremony.HandOver.Title;
                linkAutoRedirect = apiPackageSettings.Ceremony.HandOver.AutoRedirect;
                linkParameters = apiPackageSettings.Ceremony.HandOver.Parameters;
            }

            foreach (OneSpanSign.API.IntegrationFrameworkWorkflow apiIfWorkflow in apiPackageSettings.IntegrationFrameworkWorkflows)
                integrationFrameworkWorkflows.Add (IntegrationFrameworkWorkflowConverter.ToSDK(apiIfWorkflow));
        }

    }
}

