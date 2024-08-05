using System;
using System.Collections;
using System.Collections.Generic;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    public class AccountLimitSupportedLanguagesSettingsExample : SDKSample
    {
        public static void Main (string [] args)
        {
            new AccountLimitSupportedLanguagesSettingsExample().Run ();
        }

        public SupportedLanguages  DefaultLimitSupportedLanguagesSettings, PatchedLimitSupportedLanguagesSettings1, 
            PatchedLimitSupportedLanguagesSettings2, AfterDeleteLimitSupportedLanguagesSettings;
        override public void Execute ()
        {
            
            List<string> supportedLanguagesList1 = new List<string>(){"en", "fr"};
            
            SupportedLanguages supportedLanguages1 = AccountLimitSupportedLanguagesSettingsBuilder
                .NewLimitSupportedLanguagesSettings()
                .WithDefaultSignerLanguage("fr")
                .WithSignerLanguages(supportedLanguagesList1)
                .Build();
            
            List<string> supportedLanguagesList2 = new List<string>(){"en", "de"};
            
            SupportedLanguages supportedLanguages2 = AccountLimitSupportedLanguagesSettingsBuilder
                .NewLimitSupportedLanguagesSettings()
                .WithDefaultSignerLanguage("de")
                .WithSignerLanguages(supportedLanguagesList2)
                .Build();
            
            //Default Supported Languages
            DefaultLimitSupportedLanguagesSettings = OssClient.AccountConfigService.GetAccountLimitSupportedLanguagesSettings();

            
            OssClient.AccountConfigService.SaveAccountLimitSupportedLanguagesSettings(supportedLanguages1);
            PatchedLimitSupportedLanguagesSettings1 = OssClient.AccountConfigService.GetAccountLimitSupportedLanguagesSettings();
            
            OssClient.AccountConfigService.SaveAccountLimitSupportedLanguagesSettings(supportedLanguages2);
            PatchedLimitSupportedLanguagesSettings2 = OssClient.AccountConfigService.GetAccountLimitSupportedLanguagesSettings();
            
            //Delete Account Supported Languages Setting
            OssClient.AccountConfigService.DeleteAccountLimitSupportedLanguagesSettings();
            
            AfterDeleteLimitSupportedLanguagesSettings = OssClient.AccountConfigService.GetAccountLimitSupportedLanguagesSettings();
            
        } 
    }
}