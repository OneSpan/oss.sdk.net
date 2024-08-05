namespace OneSpanSign.Sdk
{
    internal class AccountLimitSupportedLanguagesConverter
    {
        private OneSpanSign.Sdk.SupportedLanguages sdkAccountLimitSupportedLanguagesSettings;
        private OneSpanSign.API.SupportedLanguages apiAccountLimitSupportedLanguagesSettings;
        
        
        public AccountLimitSupportedLanguagesConverter(OneSpanSign.API.SupportedLanguages apiAccountLimitSupportedLanguagesSettings)
        {
            this.apiAccountLimitSupportedLanguagesSettings = apiAccountLimitSupportedLanguagesSettings;
        }
        

        public AccountLimitSupportedLanguagesConverter(OneSpanSign.Sdk.SupportedLanguages sdkAccountLimitSupportedLanguagesSettings)
        {
            this.sdkAccountLimitSupportedLanguagesSettings = sdkAccountLimitSupportedLanguagesSettings;
        }
        
        
        public OneSpanSign.API.SupportedLanguages toAPIAccountLimitSupportedLanguagesSettings()
		{
			if (sdkAccountLimitSupportedLanguagesSettings == null)
			{
				return apiAccountLimitSupportedLanguagesSettings;
			}

			OneSpanSign.API.SupportedLanguages result = new OneSpanSign.API.SupportedLanguages();
			result.DefaultSignerLanguage = sdkAccountLimitSupportedLanguagesSettings.DefaultSignerLanguage;
			result.SignerLanguages = sdkAccountLimitSupportedLanguagesSettings.SignerLanguages;
            return result;
		}

        
		public SupportedLanguages toSDKAccountLimitSupportedLanguagesSettings()
		{
			if (apiAccountLimitSupportedLanguagesSettings == null)
			{
				return sdkAccountLimitSupportedLanguagesSettings;
			}

			SupportedLanguages result = new SupportedLanguages();
			result.DefaultSignerLanguage = apiAccountLimitSupportedLanguagesSettings.DefaultSignerLanguage;
			result.SignerLanguages = apiAccountLimitSupportedLanguagesSettings.SignerLanguages;
			return result;
		}
		
    }
}