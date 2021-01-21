namespace OneSpanSign.Sdk
{
	internal class NotaryWelcomeOptionsConverter
    {
		private OneSpanSign.Sdk.NotaryWelcomeOptions sdkNotaryWelcomeOptions;
		private OneSpanSign.API.NotaryWelcomeOptions apiNotaryWelcomeOptions;

		public NotaryWelcomeOptionsConverter(OneSpanSign.API.NotaryWelcomeOptions apiNotaryWelcomeOptions)
        {
			this.apiNotaryWelcomeOptions = apiNotaryWelcomeOptions;
        }

		public NotaryWelcomeOptionsConverter(OneSpanSign.Sdk.NotaryWelcomeOptions sdkNotaryWelcomeOptions)
		{
			this.sdkNotaryWelcomeOptions = sdkNotaryWelcomeOptions;
		}

		public OneSpanSign.API.NotaryWelcomeOptions ToAPINotaryWelcomeOptions()
		{
			if (sdkNotaryWelcomeOptions == null)
			{
				return apiNotaryWelcomeOptions;
			}

			OneSpanSign.API.NotaryWelcomeOptions result = new OneSpanSign.API.NotaryWelcomeOptions();

			result.Title = sdkNotaryWelcomeOptions.Title;
			result.Body = sdkNotaryWelcomeOptions.Body;
			result.RecipientName = sdkNotaryWelcomeOptions.RecipientName;
			result.RecipientEmail = sdkNotaryWelcomeOptions.RecipientEmail;
			result.RecipientActionRequired = sdkNotaryWelcomeOptions.RecipientActionRequired;
			result.NotaryTag = sdkNotaryWelcomeOptions.NotaryTag;
			result.RecipientRole = sdkNotaryWelcomeOptions.RecipientRole;
			result.RecipientStatus = sdkNotaryWelcomeOptions.RecipientStatus;

            return result;
		}

		public OneSpanSign.Sdk.NotaryWelcomeOptions ToSDKNotaryWelcomeOptions()
		{
			if (apiNotaryWelcomeOptions == null)
			{
				return sdkNotaryWelcomeOptions;
			}

			OneSpanSign.Sdk.NotaryWelcomeOptions result = new OneSpanSign.Sdk.NotaryWelcomeOptions();
			result.Title = apiNotaryWelcomeOptions.Title.Value;
			result.Body = apiNotaryWelcomeOptions.Body.Value;
			result.RecipientName = apiNotaryWelcomeOptions.RecipientName.Value;
			result.RecipientEmail = apiNotaryWelcomeOptions.RecipientEmail.Value;
			result.RecipientActionRequired = apiNotaryWelcomeOptions.RecipientActionRequired.Value;
			result.NotaryTag = apiNotaryWelcomeOptions.NotaryTag.Value;
			result.RecipientRole = apiNotaryWelcomeOptions.RecipientRole.Value;
			result.RecipientStatus = apiNotaryWelcomeOptions.RecipientStatus.Value;
			
            return result;
		}
		
    }
}

