namespace OneSpanSign.Sdk
{
	internal class ChooseSignatureSettingsConverter
    {
		private OneSpanSign.Sdk.ChooseSignatureSettings sdkChooseSignatureSettings;
		private OneSpanSign.API.ChooseSignatureSettings apiChooseSignatureSettings;

		public ChooseSignatureSettingsConverter(OneSpanSign.API.ChooseSignatureSettings apiChooseSignatureSettings)
        {
			this.apiChooseSignatureSettings = apiChooseSignatureSettings;
        }

		public ChooseSignatureSettingsConverter(OneSpanSign.Sdk.ChooseSignatureSettings sdkChooseSignatureSettings)
		{
			this.sdkChooseSignatureSettings = sdkChooseSignatureSettings;
		}

		public OneSpanSign.API.ChooseSignatureSettings ToAPIChooseSignatureSettings()
		{
			if (sdkChooseSignatureSettings == null)
			{
				return apiChooseSignatureSettings;
			}

			OneSpanSign.API.ChooseSignatureSettings result = new OneSpanSign.API.ChooseSignatureSettings();

			result.ChooseSignatureOptions = new ChooseSignatureOptionsConverter(sdkChooseSignatureSettings.Signature).ToAPIChooseSignatureOptions();
			return result;
		}

		public OneSpanSign.Sdk.ChooseSignatureSettings ToSDKChooseSignatureSettings()
		{
			if (apiChooseSignatureSettings == null)
			{
				return sdkChooseSignatureSettings;
			}

			OneSpanSign.Sdk.ChooseSignatureSettings result = new OneSpanSign.Sdk.ChooseSignatureSettings();
			result.Signature = new ChooseSignatureOptionsConverter(apiChooseSignatureSettings.ChooseSignatureOptions).ToSDKChooseSignatureOptions();

			return result;
		}
		
    }
}

