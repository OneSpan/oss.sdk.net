using OneSpanSign.Sdk;

namespace SDK.Examples
{
    public class AccountSettingsExample : SDKSample
    {
        public static void Main (string [] args)
        {
            new AccountSettingsExample().Run ();
        }

        public AccountSettings defaultAccountSettings, patchedAccountSettings, restoredAccountSettings;
        public AccountFeatureSettings defaultAccountFeatureSettings, patchedAccountFeatureSettings, restoredAccountFeatureSettings;
        public AccountPackageSettings defaultAccountPackageSettings, patchedAccountPackageSettings, restoredAccountPackageSettings;

        // The patched settings are reverted by patching back the original values instead of calling
        // DeleteAccountSettings / DeleteAccountFeatureSettings / DeleteAccountPackageSettings. A delete
        // wipes every account-level override, including features enabled from backoffice that the SDK
        // does not model (e.g. expandedClickableArea), which breaks later examples relying on them.
        override public void Execute ()
        {
            //Get Account Settings
            defaultAccountSettings = OssClient.AccountConfigService.GetAccountSettings();

            AccountSettings accountSettings = AccountSettingsBuilder.NewAccountSettings()
                .WithAccountFeatureSettings(AccountFeatureSettingsBuilder.NewAccountFeatureSettings()
                    .WithoutAllowCheckboxConsentApproval()
                    .WithAllowInPersonForAccountSenders()
                    .WithoutAttachments()
                    .WithoutConditionalFields()
                    .WithOverrideRecipientsPreferredLanguage()
                    .WithEnableRecipientHistory()
                    .WithAllowSignersDownloadEvidenceSummary()
                    .WithoutDocumentWidget()
                    .Build())
                .WithAccountPackageSettings(AccountPackageSettingsBuilder.NewAccountPackageSettings()
                    .WithAda()
                    .WithDeclineButton()
                    .WithDefaultTimeBasedExpiry()
                    .WithDisableDeclineOther()
                    .WithTitle()
                    .WithProgressBar()
                    .WithNavigator()
                    .WithUnlimitedMaxAttachmentFiles()
                    .WithDefaultFontSize()
                    .Build())
                .Build();
            //Save Account Settings
            OssClient.AccountConfigService.PatchAccountSettings(accountSettings);
            patchedAccountSettings = OssClient.AccountConfigService.GetAccountSettings();
            
            //Restore Account Settings
            OssClient.AccountConfigService.PatchAccountSettings(defaultAccountSettings);
            restoredAccountSettings = OssClient.AccountConfigService.GetAccountSettings();
            
            //Get Account Feature Settings
            defaultAccountFeatureSettings = OssClient.AccountConfigService.GetAccountFeatureSettings();
            
            AccountFeatureSettings accountFeatureSettings = AccountFeatureSettingsBuilder.NewAccountFeatureSettings()
                    .WithoutAllowCheckboxConsentApproval()
                    .WithAllowInPersonForAccountSenders()
                    .WithoutAttachments()
                    .WithoutConditionalFields()
                    .WithEnableRecipientHistory()
                    .WithAllowSignersDownloadEvidenceSummary()
                    .WithDocumentWidget()
                    .Build();
            //Save Account Feature Settings
            OssClient.AccountConfigService.PatchAccountFeatureSettings(accountFeatureSettings);
            patchedAccountFeatureSettings = OssClient.AccountConfigService.GetAccountFeatureSettings();
            
            //Restore Account Feature Settings
            OssClient.AccountConfigService.PatchAccountFeatureSettings(defaultAccountFeatureSettings);
            restoredAccountFeatureSettings = OssClient.AccountConfigService.GetAccountFeatureSettings();
            
            //Get Account Package Settings
            defaultAccountPackageSettings = OssClient.AccountConfigService.GetAccountPackageSettings();
            
            AccountPackageSettings accountPackageSettings = AccountPackageSettingsBuilder.NewAccountPackageSettings()
                .WithAda()
                .WithDeclineButton()
                .WithDefaultTimeBasedExpiry()
                .WithDisableDeclineOther()
                .WithTitle()
                .WithProgressBar()
                .WithNavigator()
                .WithCustomFontSize(16)
                .WithCustomMaxAttachmentFiles(100)
                .Build();
            //Save Account Package Settings
            OssClient.AccountConfigService.PatchAccountPackageSettings(accountPackageSettings);
            patchedAccountPackageSettings = OssClient.AccountConfigService.GetAccountPackageSettings();
            
            //Restore Account Package Settings
            OssClient.AccountConfigService.PatchAccountPackageSettings(defaultAccountPackageSettings);
            restoredAccountPackageSettings = OssClient.AccountConfigService.GetAccountPackageSettings();

        } 
    }
}