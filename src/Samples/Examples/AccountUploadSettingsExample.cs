using System.Collections.Generic;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    public class AccountUploadSettingsExample : SDKSample
    {
        public static void Main (string [] args)
        {
            new AccountUploadSettingsExample().Run ();
        }
        
        public AccountUploadSettings defaultAccountUploadSettings, patchedAccountUploadSettings, deletedAccountUploadSettings;
        override public void Execute ()
        {
            //Get Account Upload Settings
            defaultAccountUploadSettings = OssClient.AccountConfigService.GetAccountUploadSettings();

            AccountUploadSettings accountUploadSettings = AccountUploadSettingsBuilder.NewAccountUploadSettings()
                .WithAllowedFileTypes(new List<string>{"TestFileType1","TestFileType2"})
                .Build();
            
            //Update Account Upload Settings
            OssClient.AccountConfigService.UpdateAccountUploadSettings(accountUploadSettings);
            patchedAccountUploadSettings = OssClient.AccountConfigService.GetAccountUploadSettings();
            
            //Delete Account Upload Settings
            OssClient.AccountConfigService.DeleteAccountUploadSettings();
            deletedAccountUploadSettings = OssClient.AccountConfigService.GetAccountUploadSettings();
        } 
    }
}