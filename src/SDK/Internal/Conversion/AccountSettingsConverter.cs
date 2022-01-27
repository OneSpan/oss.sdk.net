namespace OneSpanSign.Sdk
{
	internal class AccountSettingsConverter
    {
		private OneSpanSign.Sdk.AccountSettings sdkAccountSettings;
		private OneSpanSign.API.AccountSettings apiAccountSettings;

		public AccountSettingsConverter(OneSpanSign.API.AccountSettings apiAccountSettings)
        {
			this.apiAccountSettings = apiAccountSettings;
        }

		public AccountSettingsConverter(OneSpanSign.Sdk.AccountSettings sdkAccountSettings)
		{
			this.sdkAccountSettings = sdkAccountSettings;
		}

		public OneSpanSign.API.AccountSettings ToAPIAccountSettings()
		{
			if (sdkAccountSettings == null)
			{
				return apiAccountSettings;
			}

			OneSpanSign.API.AccountSettings result = new OneSpanSign.API.AccountSettings();

			result.AccountFeatureSettings = new AccountFeatureSettingsConverter(sdkAccountSettings.AccountFeatureSettings).ToAPIAccountFeatureSettings();
			result.AccountPackageSettings = new AccountPackageSettingsConverter(sdkAccountSettings.AccountPackageSettings).ToAPIAccountPackageSettings();
			
			return result;
		}

		public OneSpanSign.Sdk.AccountSettings ToSDKAccountSettings()
		{
			if (apiAccountSettings == null)
			{
				return sdkAccountSettings;
			}

			OneSpanSign.Sdk.AccountSettings result = new OneSpanSign.Sdk.AccountSettings();
			result.AccountFeatureSettings = new AccountFeatureSettingsConverter(apiAccountSettings.AccountFeatureSettings).ToSDKAccountFeatureSettings();
			result.AccountPackageSettings = new AccountPackageSettingsConverter(apiAccountSettings.AccountPackageSettings).ToSDKAccountPackageSettings();
			
			return result;
		}
		
    }
}

