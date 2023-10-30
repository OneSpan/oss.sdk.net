using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class AccountDesignerSettingsExample : SDKSample
    {

        public static void Main (string [] args)
        {
            new AccountDesignerSettingsExample().Run ();
        }

        public AccountDesignerSettings defaultAccountDesignerSettings, patchedAccountDesignerSettings, deletedAccountDesignerSettings;
        override public void Execute ()
        {
            //Get Account Designer Settings
            defaultAccountDesignerSettings = OssClient.AccountConfigService.GetAccountDesignerSettings();

            AccountDesignerSettings accountDesignerSettings = AccountDesignerSettingsBuilder.NewAccountDesignerSettings()
                .WithSend()
                .WithDone()
                .WithoutSettings()
                .WithoutDocumentVisibility()
                .WithAddDocument()
                .WithEditDocument()
                .WithoutDeleteDocument()
                .WithoutAddSigner()
                .WithEditRecipient()
                .WithRolePickerSender()
                .WithoutSaveLayout()
                .WithoutApplyLayout()
                .WithShowSharedLayouts()
                .WithDefaultSignatureType("capture")
                .Build();
            
            //Save Account Designer Settings
            OssClient.AccountConfigService.PatchAccountDesignerSettings(accountDesignerSettings);
            patchedAccountDesignerSettings = OssClient.AccountConfigService.GetAccountDesignerSettings();
            
            //Delete Account Designer Settings
            OssClient.AccountConfigService.DeleteAccountDesignerSettings();
            deletedAccountDesignerSettings = OssClient.AccountConfigService.GetAccountDesignerSettings();
            
        } 
    }
}