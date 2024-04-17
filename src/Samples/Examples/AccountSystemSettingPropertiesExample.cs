using OneSpanSign.Sdk;

namespace SDK.Examples
{
    public class AccountSystemSettingPropertiesExample :  SDKSample
    {
        public static void Main (string [] args)
        {
            new AccountSystemSettingPropertiesExample().Run ();
        }

        public AccountSystemSettingProperties defaultAccountSystemSettingProperties, patchedAccountSystemSettingProperties, deletedAccountSystemSettingProperties;
        override public void Execute ()
        {
            //Get Account System Settings
            defaultAccountSystemSettingProperties = OssClient.AccountConfigService.GetAccountSystemSettingProperties();

            AccountSystemSettingProperties accountSystemSettingProperties = AccountSystemSettingPropertiesBuilder.NewAccountSystemSettingPropertiesBuilder()
                .WithSenderLoginMaxFailedAttempts(2)
                .WithSessionTimeoutWarning(200000)
                .WithLoginSessionTimeout(60000)
                .WithOrderLastNameFirstName(true)
                .Build();
            
            //Save Account System Settings
            OssClient.AccountConfigService.PatchAccountSystemSettingProperties(accountSystemSettingProperties);
            patchedAccountSystemSettingProperties = OssClient.AccountConfigService.GetAccountSystemSettingProperties();
            
            //Delete Account System Settings
            OssClient.AccountConfigService.DeleteAccountSystemSettingProperties();
            deletedAccountSystemSettingProperties = OssClient.AccountConfigService.GetAccountSystemSettingProperties();
            
        } 
    }
}