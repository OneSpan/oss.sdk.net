using System;
using OneSpanSign.API;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class DocumentPackageSettings
    {
        private Nullable<bool> showLanguageDropDown = null;
        public Nullable<bool> ShowLanguageDropDown 
        {
            get { return showLanguageDropDown; }
            set { showLanguageDropDown = value; }
        }

        private Nullable<bool> enableFirstAffidavit = null;
        public Nullable<bool> EnableFirstAffidavit 
        {
            get { return enableFirstAffidavit; }
            set { enableFirstAffidavit = value; }
        }

        private Nullable<bool> enableSecondAffidavit = null;
        public Nullable<bool> EnableSecondAffidavit 
        {
            get { return enableSecondAffidavit; }
            set { enableSecondAffidavit = value; }
        }

        private Nullable<bool> showOwnerInPersonDropDown = null;
        public Nullable<bool> ShowOwnerInPersonDropDown 
        {
            get { return showOwnerInPersonDropDown; }
            set { showOwnerInPersonDropDown = value; }
        }

        private Nullable<bool> enableInPerson = null;

        public Nullable<bool> EnableInPerson 
        {
            get { return enableInPerson; }
            set { enableInPerson = value; }
        }

        private Nullable<bool> enableOptOut = null;

        public Nullable<bool> EnableOptOut 
        {
            get { return enableOptOut; }
            set { enableOptOut = value; }
        }

        private Nullable<bool> disableOptOutOther = null;

        public Nullable<bool> DisableOptOutOther 
        {
            get { return disableOptOutOther; }
            set { disableOptOutOther = value; }
        }

        private Nullable<bool> enableDecline = null;

        public Nullable<bool> EnableDecline 
        {
            get { return enableDecline; }
            set { enableDecline = value; }
        }
        
        private Nullable<bool> expandLeftMenu = null;

        public Nullable<bool> ExpandLeftMenu 
        {
            get { return expandLeftMenu; }
            set { expandLeftMenu = value; }
        }

        private Nullable<bool> disableDeclineOther = null;

        public Nullable<bool> DisableDeclineOther 
        {
            get { return disableDeclineOther; }
            set { disableDeclineOther = value; }
        }

        private Nullable<bool> hideWatermark = null;

        public Nullable<bool> HideWatermark 
        {
            get { return hideWatermark; }
            set { hideWatermark = value; }
        }

        private Nullable<bool> hideCaptureText = null;

        public Nullable<bool> HideCaptureText 
        {
            get { return hideCaptureText; }
            set { hideCaptureText = value; }
        }

        private Nullable<bool> ada = null;

        public Nullable<bool> Ada 
        {
            get { return ada; }
            set { ada = value; }
        }

        private Nullable<Int32> fontSize = null;

        public Nullable<Int32> FontSize 
        {
            get { return fontSize; }
            set { fontSize = value; }
        }

        private Nullable<bool> enforceCaptureSignature = null;

        public Nullable<bool> EnforceCaptureSignature 
        {
            get { return enforceCaptureSignature; }
            set { enforceCaptureSignature = value; }
        }

        private Nullable<bool> defaultTimeBasedExpiry = null;

        public Nullable<bool> DefaultTimeBasedExpiry 
        {
            get { return defaultTimeBasedExpiry; }
            set { defaultTimeBasedExpiry = value; }
        }

        private Nullable<Int32> remainingDays = null;

        public Nullable<Int32> RemainingDays 
        {
            get { return remainingDays; }
            set { remainingDays = value; }
        }

        private Nullable<bool> showNseHelp = null;

        public Nullable<bool> ShowNseHelp
        {
            get { return showNseHelp; }
            set { showNseHelp = value; }
        }

        private Nullable<bool> showNseOverview = null;

        public Nullable<bool> ShowNseOverview
        {
            get { return showNseOverview; }
            set { showNseOverview = value; }
        }
        
        private Nullable<bool> showNseLogoInIframe = null;
        
        public Nullable<bool> ShowNseLogoInIframe
        {
            get { return showNseLogoInIframe; }
            set { showNseLogoInIframe = value; }
        }

        private List<string> declineReasons = new List<string> ();

        public List<string> DeclineReasons 
        {
            get { return declineReasons; }
            set { declineReasons = value; }
        }

        private List<string> optOutReasons = new List<string> ();

        public List<string> OptOutReasons 
        {
            get { return optOutReasons; }
            set { optOutReasons = value; }
        }

        private Nullable<int> maxAuthAttempts = null;

        public Nullable<int> MaxAuthAttempts 
        {
            get { return maxAuthAttempts; }
            set { maxAuthAttempts = value; }
        }

        private Nullable<bool> showDownloadButton = null;

        public Nullable<bool> ShowDownloadButton 
        {
            get { return showDownloadButton; }
            set { showDownloadButton = value; }
        }

        private Nullable<bool> showDialogOnComplete = null;

        public Nullable<bool> ShowDialogOnComplete 
        {
            get { return showDialogOnComplete; }
            set { showDialogOnComplete = value; }
        }

        private string linkText;

        public string LinkText 
        {
            get { return linkText; }
            set { linkText = value; }
        }

        private string linkTooltip;

        public string LinkTooltip 
        {
            get { return linkTooltip; }
            set { linkTooltip = value; }
        }

        private string linkHref;

        public string LinkHref 
        {
            get { return linkHref; }
            set { linkHref = value; }
        }

        private Nullable<bool> linkAutoRedirect = null;

        public Nullable<bool> LinkAutoRedirect 
        {
            get { return linkAutoRedirect; }
            set { linkAutoRedirect = value; }
        }
        
        private HashSet<String> linkParameters;
        public HashSet<String> LinkParameters 
        {
            get { return linkParameters; }
            set { linkParameters = value; }
        }

        private CeremonyLayoutSettings ceremonyLayoutSettings = null;

        public CeremonyLayoutSettings CeremonyLayoutSettings 
        {
            get { return ceremonyLayoutSettings; }
            set { ceremonyLayoutSettings = value; }
        }

        private Nullable<Int32> maxAttachmentFiles = null;

        public Nullable<Int32> MaxAttachmentFiles
        {
            get { return maxAttachmentFiles; }
            set { maxAttachmentFiles = value; }
        }

        private List<IntegrationFrameworkWorkflow> integrationFrameworkWorkflows = new List<IntegrationFrameworkWorkflow> ();

        public List<IntegrationFrameworkWorkflow> IntegrationFrameworkWorkflows 
        {
            get { return integrationFrameworkWorkflows; }
            set { integrationFrameworkWorkflows = value; }
        }

        internal PackageSettings toAPIPackageSettings ()
        {

            CeremonySettings ceremonySettings = new CeremonySettings ();

            if (enableInPerson != null)
                ceremonySettings.InPerson = enableInPerson.Value;

            if (enableOptOut != null)
                ceremonySettings.OptOutButton = enableOptOut.Value;

            if (enableDecline != null)
                ceremonySettings.DeclineButton = enableDecline.Value;

            if (expandLeftMenu != null)
                ceremonySettings.LeftMenuExpand = expandLeftMenu.Value;

            if (disableOptOutOther != null)
                ceremonySettings.DisableOptOutOther = disableOptOutOther.Value;

            if (disableDeclineOther != null)
                ceremonySettings.DisableDeclineOther = disableDeclineOther.Value;

            if (hideWatermark != null)
                ceremonySettings.HideWatermark = hideWatermark.Value;

            if (hideCaptureText != null)
                ceremonySettings.HideCaptureText = hideCaptureText.Value;

            if (enableFirstAffidavit != null)
                ceremonySettings.DisableFirstInPersonAffidavit = !enableFirstAffidavit.Value;

            if (enableSecondAffidavit != null)
                ceremonySettings.DisableSecondInPersonAffidavit = !enableSecondAffidavit.Value;

            if (showOwnerInPersonDropDown != null)
                ceremonySettings.HidePackageOwnerInPerson = !showOwnerInPersonDropDown.Value;

            if (showLanguageDropDown != null)
                ceremonySettings.HideLanguageDropdown = !showLanguageDropDown.Value;

            if (enforceCaptureSignature != null)
                ceremonySettings.EnforceCaptureSignature = enforceCaptureSignature.Value;

            if (ada != null)
                ceremonySettings.Ada = ada.Value;

            if (fontSize != null)
                ceremonySettings.FontSize = fontSize.Value;

            if (defaultTimeBasedExpiry != null)
                ceremonySettings.DefaultTimeBasedExpiry = defaultTimeBasedExpiry.Value;

            if (remainingDays != null)
                ceremonySettings.RemainingDays = remainingDays.Value;

            if (showNseHelp != null)
                            ceremonySettings.ShowNseHelp = showNseHelp.Value;

            if (showNseOverview != null)
                            ceremonySettings.ShowNseOverview = showNseOverview.Value;

            if (showNseLogoInIframe != null)
                ceremonySettings.ShowNseLogoInIframe = showNseLogoInIframe.Value;
            
            foreach (string declineReason in declineReasons)
                ceremonySettings.DeclineReasons.Add (declineReason);

            foreach (string optOutReason in optOutReasons)
                ceremonySettings.OptOutReasons.Add (optOutReason);

            if (maxAuthAttempts != null)
                ceremonySettings.MaxAuthFailsAllowed = maxAuthAttempts.Value;

            if (maxAttachmentFiles != null)
                ceremonySettings.MaxAttachmentFiles = maxAttachmentFiles.Value;

            if (linkHref != null) {
                OneSpanSign.API.Link link = new API.Link ();
                link.Href = linkHref;
                link.Text = (linkText == null ? linkHref : linkText);
                link.Title = (linkTooltip == null ? linkHref : linkTooltip);
                link.AutoRedirect = (linkAutoRedirect == null ? false : linkAutoRedirect);
                link.Parameters = linkParameters;
                ceremonySettings.HandOver = link;
            }

            if (showDialogOnComplete != null) {
                CeremonyEvents ceremonyEvents = new CeremonyEvents ();
                CeremonyEventComplete ceremonyEventComplete = new CeremonyEventComplete ();
                if (showDialogOnComplete != null)
                    ceremonyEventComplete.Dialog = showDialogOnComplete.Value;

                ceremonyEvents.Complete = ceremonyEventComplete;
                ceremonySettings.Events = ceremonyEvents;
            }

            if (showDownloadButton != null) {
                DocumentToolbarOptions documentToolbarOptions = new DocumentToolbarOptions ();
                if (showDownloadButton != null)
                    documentToolbarOptions.DownloadButton = showDownloadButton.Value;
                ceremonySettings.DocumentToolbarOptions = documentToolbarOptions;
            }

            if (ceremonyLayoutSettings != null) {
                ceremonySettings.Layout = new CeremonyLayoutSettingsConverter (ceremonyLayoutSettings).ToAPILayoutOptions ();
            }

            IList<OneSpanSign.API.IntegrationFrameworkWorkflow> apiIntegrationFrameworkWorkflows = new List<OneSpanSign.API.IntegrationFrameworkWorkflow>();
            foreach (IntegrationFrameworkWorkflow sdkIfWorkflow in integrationFrameworkWorkflows)
                apiIntegrationFrameworkWorkflows.Add (IntegrationFrameworkWorkflowConverter.ToAPI(sdkIfWorkflow));
            
            PackageSettings result = new PackageSettings ();
            result.Ceremony = ceremonySettings;
            result.IntegrationFrameworkWorkflows = apiIntegrationFrameworkWorkflows;

            return result;
        }
    }
}

