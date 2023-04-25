using System;

namespace OneSpanSign.Sdk
{
    public class AccountFeatureSettingsBuilder
    {
        private Nullable<bool> allowCheckboxConsentApproval;
        private Nullable<bool> allowInPersonForAccountSenders;
        private Nullable<bool> attachments;
        private Nullable<bool> conditionalFields;
        private Nullable<bool> customFields;
        private Nullable<bool> delegation;
        private Nullable<bool> deliverDocumentsByEmail;
        private Nullable<bool> disableFooter;
        private Nullable<bool> disableInPersonActivationEmail;
        private Nullable<bool> documentVisibility;
        private Nullable<bool> emailDocumentsAndEvidenceSummary;
        private Nullable<bool> enforceAuth;
        private Nullable<bool> evidenceSummary;
        private Nullable<bool> flattenSignerDocuments;
        private Nullable<bool> forceLogin;
        private Nullable<bool> forceTransactionOwnerLogin;
        private Nullable<bool> groups;
        private Nullable<bool> inAppReports;
        private Nullable<bool> maskResponse;
        private Nullable<bool> mobileCapture;
        private Nullable<bool> optionalSignature;
        private Nullable<bool> passwordManagement;
        private Nullable<bool> preventConsentRemoval;
        private Nullable<bool> qnaAuth;
        private Nullable<bool> sendToMobile;
        private Nullable<bool> uploadSignatureImage;
        private Nullable<bool> overrideRecipientsPreferredLanguage;
        private Nullable<bool> enableRecipientHistory;

        private AccountFeatureSettingsBuilder()
        {
        }

        public static AccountFeatureSettingsBuilder NewAccountFeatureSettings()
        {
            return new AccountFeatureSettingsBuilder();
        }


        public AccountFeatureSettingsBuilder WithAllowCheckboxConsentApproval()
        {
            this.allowCheckboxConsentApproval = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutAllowCheckboxConsentApproval()
        {
            this.allowCheckboxConsentApproval = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithAllowInPersonForAccountSenders()
        {
            this.allowInPersonForAccountSenders = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutAllowInPersonForAccountSenders()
        {
            this.allowInPersonForAccountSenders = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithAttachments()
        {
            this.attachments = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutAttachments()
        {
            this.attachments = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithConditionalFields()
        {
            this.conditionalFields = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutConditionalFields()
        {
            this.conditionalFields = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithCustomFields()
        {
            this.customFields = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutCustomFields()
        {
            this.customFields = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithDelegation()
        {
            this.delegation = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutDelegation()
        {
            this.delegation = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithDeliverDocumentsByEmail()
        {
            this.deliverDocumentsByEmail = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutDeliverDocumentsByEmail()
        {
            this.deliverDocumentsByEmail = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithDisableFooter()
        {
            this.disableFooter = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutDisableFooter()
        {
            this.disableFooter = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithDisableInPersonActivationEmail()
        {
            this.disableInPersonActivationEmail = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutDisableInPersonActivationEmail()
        {
            this.disableInPersonActivationEmail = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithDocumentVisibility()
        {
            this.documentVisibility = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutDocumentVisibility()
        {
            this.documentVisibility = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithEmailDocumentsAndEvidenceSummary()
        {
            this.emailDocumentsAndEvidenceSummary = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutEmailDocumentsAndEvidenceSummary()
        {
            this.emailDocumentsAndEvidenceSummary = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithEnforceAuth()
        {
            this.enforceAuth = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutEnforceAuth()
        {
            this.enforceAuth = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithEvidenceSummary()
        {
            this.evidenceSummary = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutEvidenceSummary()
        {
            this.evidenceSummary = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithFlattenSignerDocuments()
        {
            this.flattenSignerDocuments = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutFlattenSignerDocuments()
        {
            this.flattenSignerDocuments = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithForceLogin()
        {
            this.forceLogin = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutForceLogin()
        {
            this.forceLogin = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithForceTransactionOwnerLogin()
        {
            this.forceTransactionOwnerLogin = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutForceTransactionOwnerLogin()
        {
            this.forceTransactionOwnerLogin = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithGroups()
        {
            this.groups = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutGroups()
        {
            this.groups = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithInAppReports()
        {
            this.inAppReports = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutInAppReports()
        {
            this.inAppReports = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithMaskResponse()
        {
            this.maskResponse = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutMaskResponse()
        {
            this.maskResponse = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithMobileCapture()
        {
            this.mobileCapture = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutMobileCapture()
        {
            this.mobileCapture = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithOptionalSignature()
        {
            this.optionalSignature = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutOptionalSignature()
        {
            this.optionalSignature = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithPasswordManagement()
        {
            this.passwordManagement = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutPasswordManagement()
        {
            this.passwordManagement = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithPreventConsentRemoval()
        {
            this.preventConsentRemoval = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutPreventConsentRemoval()
        {
            this.preventConsentRemoval = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithQnaAuth()
        {
            this.qnaAuth = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutQnaAuth()
        {
            this.qnaAuth = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithSendToMobile()
        {
            this.sendToMobile = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutSendToMobile()
        {
            this.sendToMobile = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithUploadSignatureImage()
        {
            this.uploadSignatureImage = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutUploadSignatureImage()
        {
            this.uploadSignatureImage = false;
            return this;
        }

        public AccountFeatureSettingsBuilder WithOverrideRecipientsPreferredLanguage()
        {
            this.overrideRecipientsPreferredLanguage = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutOverrideRecipientsPreferredLanguage()
        {
            this.overrideRecipientsPreferredLanguage = false;
            return this;
        }
        
        public AccountFeatureSettingsBuilder WithEnableRecipientHistory()
        {
            this.enableRecipientHistory = true;
            return this;
        }

        public AccountFeatureSettingsBuilder WithoutEnableRecipientHistory()
        {
            this.enableRecipientHistory = false;
            return this;
        }


        public AccountFeatureSettings Build()
        {
            AccountFeatureSettings result = new AccountFeatureSettings();
            result.AllowCheckboxConsentApproval = allowCheckboxConsentApproval;
            result.AllowInPersonForAccountSenders = allowInPersonForAccountSenders;
            result.Attachments = attachments;
            result.ConditionalFields = conditionalFields;
            result.CustomFields = customFields;
            result.Delegation = delegation;
            result.DeliverDocumentsByEmail = deliverDocumentsByEmail;
            result.DisableFooter = disableFooter;
            result.DisableInPersonActivationEmail = disableInPersonActivationEmail;
            result.DocumentVisibility = documentVisibility;
            result.EmailDocumentsAndEvidenceSummary = emailDocumentsAndEvidenceSummary;
            result.EnforceAuth = enforceAuth;
            result.EvidenceSummary = evidenceSummary;
            result.FlattenSignerDocuments = flattenSignerDocuments;
            result.ForceLogin = forceLogin;
            result.ForceTransactionOwnerLogin = forceTransactionOwnerLogin;
            result.Groups = groups;
            result.InAppReports = inAppReports;
            result.MaskResponse = maskResponse;
            result.MobileCapture = mobileCapture;
            result.OptionalSignature = optionalSignature;
            result.PasswordManagement = passwordManagement;
            result.PreventConsentRemoval = preventConsentRemoval;
            result.QnaAuth = qnaAuth;
            result.SendToMobile = sendToMobile;
            result.UploadSignatureImage = uploadSignatureImage;
            result.OverrideRecipientsPreferredLanguage = overrideRecipientsPreferredLanguage;
            result.EnableRecipientHistory = enableRecipientHistory;

            return result;
        }
    }
}