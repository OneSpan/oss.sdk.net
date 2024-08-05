using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class AccountLimitSupportedLanguagesSettingsBuilder
    {
        private string _defaultSignerLanguage;
        private List<string> _signerLanguages;
       
       
        private AccountLimitSupportedLanguagesSettingsBuilder()
        {
        }

        public static AccountLimitSupportedLanguagesSettingsBuilder NewLimitSupportedLanguagesSettings() {
            return new AccountLimitSupportedLanguagesSettingsBuilder();
        }

        public AccountLimitSupportedLanguagesSettingsBuilder WithDefaultSignerLanguage(string defaultSignerLanguage) {
            this._defaultSignerLanguage = defaultSignerLanguage;
            return this;
        }
        
        public AccountLimitSupportedLanguagesSettingsBuilder WithSignerLanguages(List<string> signerLanguages) {
            this._signerLanguages = signerLanguages;
            return this;
        }
        
        public SupportedLanguages Build() {
            SupportedLanguages result = new SupportedLanguages();
            result.DefaultSignerLanguage = _defaultSignerLanguage;
            result.SignerLanguages = _signerLanguages;
            return result;
        }
    }
}