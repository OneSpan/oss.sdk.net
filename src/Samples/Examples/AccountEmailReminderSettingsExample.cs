using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class AccountEmailReminderSettingsExample : SDKSample
    {
        public static void Main (string [] args)
        {
            new AccountEmailReminderSettingsExample().Run ();
        }
        
        public AccountEmailReminderSettings defaultAccountEmailReminderSettings, patchedAccountEmailReminderSettings, deletedAccountEmailReminderSettings;
        override public void Execute ()
        {
            //Get Account Email Reminder Settings
            defaultAccountEmailReminderSettings = OssClient.AccountConfigService.GetAccountEmailReminderSettings();

            AccountEmailReminderSettings accountEmailReminderSettings = AccountEmailReminderSettingsBuilder.NewAccountEmailReminderSettings()
                .WithRepetitionsCount(2)
                .WithIntervalInDays(400)
                .WithStartInDaysDelay(20)
                .Build();
            
            //Save Account Email Reminder Settings
            OssClient.AccountConfigService.PatchAccountEmailReminderSettings(accountEmailReminderSettings);
            patchedAccountEmailReminderSettings = OssClient.AccountConfigService.GetAccountEmailReminderSettings();
            
            //Delete Account Email Reminder Settings
            OssClient.AccountConfigService.DeleteAccountEmailReminderSettings();
            deletedAccountEmailReminderSettings = OssClient.AccountConfigService.GetAccountEmailReminderSettings();
            
        } 
    }
}