namespace OneSpanSign.Sdk
{
	internal class InpersonWelcomeOptionsConverter
    {
		private OneSpanSign.Sdk.InpersonWelcomeOptions sdkInpersonWelcomeOptions;
		private OneSpanSign.API.InpersonWelcomeOptions apiInpersonWelcomeOptions;

		public InpersonWelcomeOptionsConverter(OneSpanSign.API.InpersonWelcomeOptions apiInpersonWelcomeOptions)
        {
			this.apiInpersonWelcomeOptions = apiInpersonWelcomeOptions;
        }

		public InpersonWelcomeOptionsConverter(OneSpanSign.Sdk.InpersonWelcomeOptions sdkInpersonWelcomeOptions)
		{
			this.sdkInpersonWelcomeOptions = sdkInpersonWelcomeOptions;
		}

		public OneSpanSign.API.InpersonWelcomeOptions ToAPIInpersonWelcomeOptions()
		{
			if (sdkInpersonWelcomeOptions == null)
			{
				return apiInpersonWelcomeOptions;
			}

			OneSpanSign.API.InpersonWelcomeOptions result = new OneSpanSign.API.InpersonWelcomeOptions();

			result.Title = sdkInpersonWelcomeOptions.Title;
			result.Body = sdkInpersonWelcomeOptions.Body;
			result.RecipientName = sdkInpersonWelcomeOptions.RecipientName;
			result.RecipientEmail = sdkInpersonWelcomeOptions.RecipientEmail;
			result.RecipientActionRequired = sdkInpersonWelcomeOptions.RecipientActionRequired;
			result.RecipientRole = sdkInpersonWelcomeOptions.RecipientRole;
			result.RecipientStatus = sdkInpersonWelcomeOptions.RecipientStatus;

            return result;
		}

		public OneSpanSign.Sdk.InpersonWelcomeOptions ToSDKInpersonWelcomeOptions()
		{
			if (apiInpersonWelcomeOptions == null)
			{
				return sdkInpersonWelcomeOptions;
			}

			OneSpanSign.Sdk.InpersonWelcomeOptions result = new OneSpanSign.Sdk.InpersonWelcomeOptions();
			result.Title = apiInpersonWelcomeOptions.Title.Value;
			result.Body = apiInpersonWelcomeOptions.Body.Value;
			result.RecipientName = apiInpersonWelcomeOptions.RecipientName.Value;
			result.RecipientEmail = apiInpersonWelcomeOptions.RecipientEmail.Value;
			result.RecipientActionRequired = apiInpersonWelcomeOptions.RecipientActionRequired.Value;
			result.RecipientRole = apiInpersonWelcomeOptions.RecipientRole.Value;
			result.RecipientStatus = apiInpersonWelcomeOptions.RecipientStatus.Value;
			
            return result;
		}
		
    }
}

