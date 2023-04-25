namespace OneSpanSign.Sdk
{
	internal class AccountFeatureSettingsConverter
    {
		private OneSpanSign.Sdk.AccountFeatureSettings sdkAccountFeatureSettings;
		private OneSpanSign.API.AccountFeatureSettings apiAccountFeatureSettings;

		public AccountFeatureSettingsConverter(OneSpanSign.API.AccountFeatureSettings apiAccountFeatureSettings)
        {
			this.apiAccountFeatureSettings = apiAccountFeatureSettings;
        }

		public AccountFeatureSettingsConverter(OneSpanSign.Sdk.AccountFeatureSettings sdkAccountFeatureSettings)
		{
			this.sdkAccountFeatureSettings = sdkAccountFeatureSettings;
		}

		public OneSpanSign.API.AccountFeatureSettings ToAPIAccountFeatureSettings()
		{
			if (sdkAccountFeatureSettings == null)
			{
				return apiAccountFeatureSettings;
			}

			OneSpanSign.API.AccountFeatureSettings result = new OneSpanSign.API.AccountFeatureSettings();

			result.AllowCheckboxConsentApproval = sdkAccountFeatureSettings.AllowCheckboxConsentApproval;
			result.AllowInPersonForAccountSenders = sdkAccountFeatureSettings.AllowInPersonForAccountSenders;
			result.Attachments = sdkAccountFeatureSettings.Attachments;
			result.ConditionalFields = sdkAccountFeatureSettings.ConditionalFields;
			result.CustomFields = sdkAccountFeatureSettings.CustomFields;
			result.Delegation = sdkAccountFeatureSettings.Delegation;
			result.DeliverDocumentsByEmail = sdkAccountFeatureSettings.DeliverDocumentsByEmail;
			result.DisableFooter = sdkAccountFeatureSettings.DisableFooter;
			result.DisableInPersonActivationEmail = sdkAccountFeatureSettings.DisableInPersonActivationEmail;
			result.DocumentVisibility = sdkAccountFeatureSettings.DocumentVisibility;
			result.EmailDocumentsAndEvidenceSummary = sdkAccountFeatureSettings.EmailDocumentsAndEvidenceSummary;
			result.EnforceAuth = sdkAccountFeatureSettings.EnforceAuth;
			result.EvidenceSummary = sdkAccountFeatureSettings.EvidenceSummary;
			result.FlattenSignerDocuments = sdkAccountFeatureSettings.FlattenSignerDocuments;
			result.ForceLogin = sdkAccountFeatureSettings.ForceLogin;
			result.ForceTransactionOwnerLogin = sdkAccountFeatureSettings.ForceTransactionOwnerLogin;
			result.Groups = sdkAccountFeatureSettings.Groups;
			result.InAppReports = sdkAccountFeatureSettings.InAppReports;
			result.MaskResponse = sdkAccountFeatureSettings.MaskResponse;
			result.MobileCapture = sdkAccountFeatureSettings.MobileCapture;
			result.OptionalSignature = sdkAccountFeatureSettings.OptionalSignature;
			result.PasswordManagement = sdkAccountFeatureSettings.PasswordManagement;
			result.PreventConsentRemoval = sdkAccountFeatureSettings.PreventConsentRemoval;
			result.QnaAuth = sdkAccountFeatureSettings.QnaAuth;
			result.SendToMobile = sdkAccountFeatureSettings.SendToMobile;
			result.UploadSignatureImage = sdkAccountFeatureSettings.UploadSignatureImage;
			result.OverrideRecipientsPreferredLanguage = sdkAccountFeatureSettings.OverrideRecipientsPreferredLanguage;
			result.EnableRecipientHistory = sdkAccountFeatureSettings.EnableRecipientHistory;
			
            return result;
		}

		public OneSpanSign.Sdk.AccountFeatureSettings ToSDKAccountFeatureSettings()
		{
			if (apiAccountFeatureSettings == null)
			{
				return sdkAccountFeatureSettings;
			}

			OneSpanSign.Sdk.AccountFeatureSettings result = new OneSpanSign.Sdk.AccountFeatureSettings();
			result.AllowCheckboxConsentApproval = apiAccountFeatureSettings.AllowCheckboxConsentApproval;
			result.AllowInPersonForAccountSenders = apiAccountFeatureSettings.AllowInPersonForAccountSenders;
			result.Attachments = apiAccountFeatureSettings.Attachments;
			result.ConditionalFields = apiAccountFeatureSettings.ConditionalFields;
			result.CustomFields = apiAccountFeatureSettings.CustomFields;
			result.Delegation = apiAccountFeatureSettings.Delegation;
			result.DeliverDocumentsByEmail = apiAccountFeatureSettings.DeliverDocumentsByEmail;
			result.DisableFooter = apiAccountFeatureSettings.DisableFooter;
			result.DisableInPersonActivationEmail = apiAccountFeatureSettings.DisableInPersonActivationEmail;
			result.DocumentVisibility = apiAccountFeatureSettings.DocumentVisibility;
			result.EmailDocumentsAndEvidenceSummary = apiAccountFeatureSettings.EmailDocumentsAndEvidenceSummary;
			result.EnforceAuth = apiAccountFeatureSettings.EnforceAuth;
			result.EvidenceSummary = apiAccountFeatureSettings.EvidenceSummary;
			result.FlattenSignerDocuments = apiAccountFeatureSettings.FlattenSignerDocuments;
			result.ForceLogin = apiAccountFeatureSettings.ForceLogin;
			result.ForceTransactionOwnerLogin = apiAccountFeatureSettings.ForceTransactionOwnerLogin;
			result.Groups = apiAccountFeatureSettings.Groups;
			result.InAppReports = apiAccountFeatureSettings.InAppReports;
			result.MaskResponse = apiAccountFeatureSettings.MaskResponse;
			result.MobileCapture = apiAccountFeatureSettings.MobileCapture;
			result.OptionalSignature = apiAccountFeatureSettings.OptionalSignature;
			result.PasswordManagement = apiAccountFeatureSettings.PasswordManagement;
			result.PreventConsentRemoval = apiAccountFeatureSettings.PreventConsentRemoval;
			result.QnaAuth = apiAccountFeatureSettings.QnaAuth;
			result.SendToMobile = apiAccountFeatureSettings.SendToMobile;
			result.UploadSignatureImage = apiAccountFeatureSettings.UploadSignatureImage;
			result.OverrideRecipientsPreferredLanguage = apiAccountFeatureSettings.OverrideRecipientsPreferredLanguage;
			result.EnableRecipientHistory = apiAccountFeatureSettings.EnableRecipientHistory;
			return result;
		}
		
    }
}

