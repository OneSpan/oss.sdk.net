using System;

namespace OneSpanSign.Sdk
{
    public class AccountPackageSettingsBuilder
    {
        private Nullable<bool> ada;
        private Nullable<bool> declineButton;
        private Nullable<bool> defaultTimeBasedExpiry;
        private Nullable<bool> disableDeclineOther;
        private Nullable<bool> disableDownloadForUncompletedPackage;
        private Nullable<bool> disableFirstInPersonAffidavit;
        private Nullable<bool> disableInPersonAffidavit;
        private Nullable<bool> disableSecondInPersonAffidavit;
        private Nullable<bool> enforceCaptureSignature;
        private Nullable<bool> extractAcroFields;
        private Nullable<bool> extractTextTags;
        private Nullable<bool> globalActionsDownload;
        private Nullable<bool> globalActionsHideEvidenceSummary;
        private Nullable<bool> hideCaptureText;
        private Nullable<bool> hideLanguageDropdown;
        private Nullable<bool> hidePackageOwnerInPerson;
        private Nullable<bool> hideWatermark;
        private Nullable<bool> inPerson;
        private Nullable<bool> leftMenuExpand;
        private Nullable<bool> optionalNavigation;
        private Nullable<bool> showNseHelp;
        private Nullable<bool> showNseLogoInIframe;
        private Nullable<bool> showNseOverview;
        private Nullable<bool> title;
        private Nullable<bool> progressBar;
        private Nullable<bool> navigator;
        private Nullable<int> maxAttachmentFiles;
        private Nullable<int> fontSize;
        
        private AccountPackageSettingsBuilder()
        {
        }

        public static AccountPackageSettingsBuilder NewAccountPackageSettings() {
            return new AccountPackageSettingsBuilder();
        }

        public AccountPackageSettingsBuilder WithAda() {
            this.ada = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutAda() {
            this.ada = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithDeclineButton() {
            this.declineButton = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutDeclineButton() {
            this.declineButton = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithDefaultTimeBasedExpiry() {
            this.defaultTimeBasedExpiry = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutDefaultTimeBasedExpiry() {
            this.defaultTimeBasedExpiry = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithDisableDeclineOther() {
            this.disableDeclineOther = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutDisableDeclineOther() {
            this.disableDeclineOther = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithDisableDownloadForUncompletedPackage() {
            this.disableDownloadForUncompletedPackage = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutDisableDownloadForUncompletedPackage() {
            this.disableDownloadForUncompletedPackage = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithDisableFirstInPersonAffidavit() {
            this.disableFirstInPersonAffidavit = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutDisableFirstInPersonAffidavit() {
            this.disableFirstInPersonAffidavit = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithDisableInPersonAffidavit() {
            this.disableInPersonAffidavit = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutDisableInPersonAffidavit() {
            this.disableInPersonAffidavit = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithDisableSecondInPersonAffidavit() {
            this.disableSecondInPersonAffidavit = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutDisableSecondInPersonAffidavit() {
            this.disableSecondInPersonAffidavit = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithEnforceCaptureSignature() {
            this.enforceCaptureSignature = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutEnforceCaptureSignature() {
            this.enforceCaptureSignature = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithExtractAcroFields() {
            this.extractAcroFields = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutExtractAcroFields() {
            this.extractAcroFields = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithExtractTextTags() {
            this.extractTextTags = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutExtractTextTags() {
            this.extractTextTags = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithGlobalActionsDownload() {
            this.globalActionsDownload = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutGlobalActionsDownload() {
            this.globalActionsDownload = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithGlobalActionsHideEvidenceSummary() {
            this.globalActionsHideEvidenceSummary = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutGlobalActionsHideEvidenceSummary() {
            this.globalActionsHideEvidenceSummary = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithHideCaptureText() {
            this.hideCaptureText = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutHideCaptureText() {
            this.hideCaptureText = false;
            return this;
        }
        
        public AccountPackageSettingsBuilder WithHideLanguageDropdown() {
            this.hideLanguageDropdown = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutHideLanguageDropdown() {
            this.hideLanguageDropdown = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithHidePackageOwnerInPerson() {
            this.hidePackageOwnerInPerson = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutHidePackageOwnerInPerson() {
            this.hidePackageOwnerInPerson = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithHideWatermark() {
            this.hideWatermark = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutHideWatermark() {
            this.hideWatermark = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithInPerson() {
            this.inPerson = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutInPerson() {
            this.inPerson = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithLeftMenuExpand() {
            this.leftMenuExpand = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutLeftMenuExpand() {
            this.leftMenuExpand = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithOptionalNavigation() {
            this.optionalNavigation = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutOptionalNavigation() {
            this.optionalNavigation = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithShowNseHelp() {
            this.showNseHelp = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutShowNseHelp() {
            this.showNseHelp = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithShowNseLogoInIframe() {
            this.showNseLogoInIframe = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutShowNseLogoInIframe() {
            this.showNseLogoInIframe = false;
            return this;
        }

        public AccountPackageSettingsBuilder WithShowNseOverview() {
            this.showNseOverview = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutShowNseOverview() {
            this.showNseOverview = false;
            return this;
        }
        public AccountPackageSettingsBuilder WithTitle() {
            this.title = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutTitle() {
            this.title = false;
            return this;
        }
        
        public AccountPackageSettingsBuilder WithProgressBar() {
            this.progressBar = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutProgressBar() {
            this.progressBar = false;
            return this;
        }
        
        public AccountPackageSettingsBuilder WithNavigator() {
            this.navigator = true;
            return this;
        }

        public AccountPackageSettingsBuilder WithoutNavigator() {
            this.navigator = false;
            return this;
        }
        
        public AccountPackageSettingsBuilder WithUnlimitedMaxAttachmentFiles()
        {
            this.maxAttachmentFiles = 0;
            return this;
        }
        
        public AccountPackageSettingsBuilder WithCustomMaxAttachmentFiles(int value)
        {
            this.maxAttachmentFiles = value;
            return this;
        }

        public AccountPackageSettingsBuilder WithDefaultFontSize() {
            this.fontSize = 12;
            return this;
        }
        public AccountPackageSettingsBuilder WithCustomFontSize(int value) {
            this.fontSize = value;
            return this;
        }
        public AccountPackageSettings Build() {
            AccountPackageSettings result = new AccountPackageSettings();
            
            result.Ada = ada;
            result.DeclineButton = declineButton;
            result.DefaultTimeBasedExpiry = defaultTimeBasedExpiry;
            result.DisableDeclineOther = disableDeclineOther;
            result.DisableDownloadForUncompletedPackage = disableDownloadForUncompletedPackage;
            result.DisableFirstInPersonAffidavit = disableFirstInPersonAffidavit;
            result.DisableInPersonAffidavit = disableInPersonAffidavit;
            result.DisableSecondInPersonAffidavit = disableSecondInPersonAffidavit;
            result.EnforceCaptureSignature = enforceCaptureSignature;
            result.ExtractAcroFields = extractAcroFields;
            result.ExtractTextTags = extractTextTags;
            result.GlobalActionsDownload = globalActionsDownload;
            result.GlobalActionsHideEvidenceSummary = globalActionsHideEvidenceSummary;
            result.HideCaptureText = hideCaptureText;
            result.HideLanguageDropdown = hideLanguageDropdown;
            result.HidePackageOwnerInPerson = hidePackageOwnerInPerson;
            result.HideWatermark = hideWatermark;
            result.InPerson = inPerson;
            result.LeftMenuExpand = leftMenuExpand;
            result.OptionalNavigation = optionalNavigation;
            result.ShowNseHelp = showNseHelp;
            result.ShowNseLogoInIframe = showNseLogoInIframe;
            result.ShowNseOverview = showNseOverview;
            result.Title = title;
            result.ProgressBar = progressBar;
            result.Navigator = navigator;
            result.MaxAttachmentFiles = maxAttachmentFiles;
            result.FontSize = fontSize;
                
            return result;
        }
    }
}