namespace OneSpanSign.Sdk
{
	internal class NotaryHostThankYouOptionsConverter
    {
		private OneSpanSign.Sdk.NotaryHostThankYouOptions sdkNotaryHostThankYouOptions;
		private OneSpanSign.API.NotaryHostThankYouOptions apiNotaryHostThankYouOptions;

		public NotaryHostThankYouOptionsConverter(OneSpanSign.API.NotaryHostThankYouOptions apiNotaryHostThankYouOptions)
        {
			this.apiNotaryHostThankYouOptions = apiNotaryHostThankYouOptions;
        }

		public NotaryHostThankYouOptionsConverter(OneSpanSign.Sdk.NotaryHostThankYouOptions sdkNotaryHostThankYouOptions)
		{
			this.sdkNotaryHostThankYouOptions = sdkNotaryHostThankYouOptions;
		}

		public OneSpanSign.API.NotaryHostThankYouOptions ToAPINotaryHostThankYouOptions()
		{
			if (sdkNotaryHostThankYouOptions == null)
			{
				return apiNotaryHostThankYouOptions;
			}

			OneSpanSign.API.NotaryHostThankYouOptions result = new OneSpanSign.API.NotaryHostThankYouOptions();

			result.Title = sdkNotaryHostThankYouOptions.Title;
			result.Body = sdkNotaryHostThankYouOptions.Body;
			result.RecipientName = sdkNotaryHostThankYouOptions.RecipientName;
			result.RecipientEmail = sdkNotaryHostThankYouOptions.RecipientEmail;
			result.RecipientRole = sdkNotaryHostThankYouOptions.RecipientRole;
			result.NotaryTag = sdkNotaryHostThankYouOptions.NotaryTag;
			result.RecipientStatus = sdkNotaryHostThankYouOptions.RecipientStatus;
			result.DownloadButton = sdkNotaryHostThankYouOptions.DownloadButton;
			result.ReviewDocumentsButton = sdkNotaryHostThankYouOptions.ReviewDocumentsButton;

            return result;
		}

		public OneSpanSign.Sdk.NotaryHostThankYouOptions ToSDKNotaryHostThankYouOptions()
		{
			if (apiNotaryHostThankYouOptions == null)
			{
				return sdkNotaryHostThankYouOptions;
			}

			OneSpanSign.Sdk.NotaryHostThankYouOptions result = new OneSpanSign.Sdk.NotaryHostThankYouOptions();
			result.Title = apiNotaryHostThankYouOptions.Title.Value;
			result.Body = apiNotaryHostThankYouOptions.Body.Value;
			result.RecipientName = apiNotaryHostThankYouOptions.RecipientName.Value;
			result.RecipientEmail = apiNotaryHostThankYouOptions.RecipientEmail.Value;
			result.RecipientRole = apiNotaryHostThankYouOptions.RecipientRole.Value;
			result.NotaryTag = apiNotaryHostThankYouOptions.NotaryTag.Value;
			result.RecipientStatus = apiNotaryHostThankYouOptions.RecipientStatus.Value;
			result.DownloadButton = apiNotaryHostThankYouOptions.DownloadButton.Value;
			result.ReviewDocumentsButton = apiNotaryHostThankYouOptions.ReviewDocumentsButton.Value;
			
            return result;
		}
		
    }
}

