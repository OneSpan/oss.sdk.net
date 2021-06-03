namespace OneSpanSign.Sdk
{
	internal class SigningUiOptionsConverter
    {
		private OneSpanSign.Sdk.SigningUiOptions sdkSigningUiOptions;
		private OneSpanSign.API.SigningUiOptions apiSigningUiOptions;

		public SigningUiOptionsConverter(OneSpanSign.API.SigningUiOptions apiSigningUiOptions)
        {
			this.apiSigningUiOptions = apiSigningUiOptions;
        }

		public SigningUiOptionsConverter(OneSpanSign.Sdk.SigningUiOptions sdkSigningUiOptions)
		{
			this.sdkSigningUiOptions = sdkSigningUiOptions;
		}

		public OneSpanSign.API.SigningUiOptions ToAPISigningUiOptions()
		{
			if (sdkSigningUiOptions == null)
			{
				return apiSigningUiOptions;
			}

			OneSpanSign.API.SigningUiOptions result = new OneSpanSign.API.SigningUiOptions();

			result.CompleteSummaryOptions = new CompleteSummaryOptionsConverter(sdkSigningUiOptions.CompleteSummaryOptions).ToAPICompleteSummaryOptions();
			result.InpersonWelcomeOptions = new InpersonWelcomeOptionsConverter(sdkSigningUiOptions.InpersonWelcomeOptions).ToAPIInpersonWelcomeOptions();
			result.InpersonHostThankYouOptions = new InpersonHostThankYouOptionsConverter(sdkSigningUiOptions.InpersonHostThankYouOptions).ToAPIInpersonHostThankYouOptions();
			result.NotaryWelcomeOptions = new NotaryWelcomeOptionsConverter(sdkSigningUiOptions.NotaryWelcomeOptions).ToAPINotaryWelcomeOptions();
			result.NotaryHostThankYouOptions = new NotaryHostThankYouOptionsConverter(sdkSigningUiOptions.NotaryHostThankYouOptions).ToAPINotaryHostThankYouOptions();
			result.OverviewOptions = new OverviewOptionsConverter(sdkSigningUiOptions.OverviewOptions).ToAPIOverviewOptions();

			return result;
		}

		public OneSpanSign.Sdk.SigningUiOptions ToSDKSigningUiOptions()
		{
			if (apiSigningUiOptions == null)
			{
				return sdkSigningUiOptions;
			}

			OneSpanSign.Sdk.SigningUiOptions result = new OneSpanSign.Sdk.SigningUiOptions();
			result.CompleteSummaryOptions = new CompleteSummaryOptionsConverter(apiSigningUiOptions.CompleteSummaryOptions).ToSDKCompleteSummaryOptions();
			result.InpersonWelcomeOptions = new InpersonWelcomeOptionsConverter(apiSigningUiOptions.InpersonWelcomeOptions).ToSDKInpersonWelcomeOptions();
			result.InpersonHostThankYouOptions = new InpersonHostThankYouOptionsConverter(apiSigningUiOptions.InpersonHostThankYouOptions).ToSDKInpersonHostThankYouOptions();
			result.NotaryWelcomeOptions = new NotaryWelcomeOptionsConverter(apiSigningUiOptions.NotaryWelcomeOptions).ToSDKNotaryWelcomeOptions();
			result.NotaryHostThankYouOptions = new NotaryHostThankYouOptionsConverter(apiSigningUiOptions.NotaryHostThankYouOptions).ToSDKNotaryHostThankYouOptions();
			result.OverviewOptions = new OverviewOptionsConverter(apiSigningUiOptions.OverviewOptions).ToSDKOverviewOptions();

			return result;
		}
		
    }
}

