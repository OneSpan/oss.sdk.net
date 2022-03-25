using OneSpanSign.Sdk;

namespace SDK.Examples
{
    public class AccountSettingsExample : SDKSample
    {
        public static void Main (string [] args)
        {
            new AccountSettingsExample().Run ();
        }

        public AccountSettings defaultAccountSettings, patchedAccountSettings, deletedAccountSettings;
        public AccountFeatureSettings defaultAccountFeatureSettings, patchedAccountFeatureSettings, deletedAccountFeatureSettings;
        public AccountPackageSettings defaultAccountPackageSettings, patchedAccountPackageSettings, deletedAccountPackageSettings;
        
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
                    .Build())
                .WithAccountPackageSettings(AccountPackageSettingsBuilder.NewAccountPackageSettings()
                    .WithAda()
                    .WithDeclineButton()
                    .WithDefaultTimeBasedExpiry()
                    .WithDisableDeclineOther()
                    .Build())
                .Build();
            //Save Account Settings
            OssClient.AccountConfigService.PatchAccountSettings(accountSettings);
            patchedAccountSettings = OssClient.AccountConfigService.GetAccountSettings();
            
            //Delete Account Settings
            OssClient.AccountConfigService.DeleteAccountSettings();
            deletedAccountSettings = OssClient.AccountConfigService.GetAccountSettings();
            
            //Get Account Feature Settings
            defaultAccountFeatureSettings = OssClient.AccountConfigService.GetAccountFeatureSettings();
            
            AccountFeatureSettings accountFeatureSettings = AccountFeatureSettingsBuilder.NewAccountFeatureSettings()
                    .WithoutAllowCheckboxConsentApproval()
                    .WithAllowInPersonForAccountSenders()
                    .WithoutAttachments()
                    .WithoutConditionalFields()
                    .Build();
            //Save Account Feature Settings
            OssClient.AccountConfigService.PatchAccountFeatureSettings(accountFeatureSettings);
            patchedAccountFeatureSettings = OssClient.AccountConfigService.GetAccountFeatureSettings();
            
            //Delete Account Feature Settings
            OssClient.AccountConfigService.DeleteAccountFeatureSettings();
            deletedAccountFeatureSettings = OssClient.AccountConfigService.GetAccountFeatureSettings();
            
            //Get Account Package Settings
            defaultAccountPackageSettings = OssClient.AccountConfigService.GetAccountPackageSettings();
            
            AccountPackageSettings accountPackageSettings = AccountPackageSettingsBuilder.NewAccountPackageSettings()
                .WithAda()
                .WithDeclineButton()
                .WithDefaultTimeBasedExpiry()
                .WithDisableDeclineOther()
                .Build();
            //Save Account Package Settings
            OssClient.AccountConfigService.PatchAccountPackageSettings(accountPackageSettings);
            patchedAccountPackageSettings = OssClient.AccountConfigService.GetAccountPackageSettings();
            
            //Delete Account Package Settings
            OssClient.AccountConfigService.DeleteAccountPackageSettings();
            deletedAccountPackageSettings = OssClient.AccountConfigService.GetAccountPackageSettings();

        } 
    }
}