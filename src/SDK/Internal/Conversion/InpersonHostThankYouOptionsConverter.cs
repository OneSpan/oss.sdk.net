namespace OneSpanSign.Sdk
{
	internal class InpersonHostThankYouOptionsConverter
    {
		private OneSpanSign.Sdk.InpersonHostThankYouOptions sdkInpersonHostThankYouOptions;
		private OneSpanSign.API.InpersonHostThankYouOptions apiInpersonHostThankYouOptions;

		public InpersonHostThankYouOptionsConverter(OneSpanSign.API.InpersonHostThankYouOptions apiInpersonHostThankYouOptions)
        {
			this.apiInpersonHostThankYouOptions = apiInpersonHostThankYouOptions;
        }

		public InpersonHostThankYouOptionsConverter(OneSpanSign.Sdk.InpersonHostThankYouOptions sdkInpersonHostThankYouOptions)
		{
			this.sdkInpersonHostThankYouOptions = sdkInpersonHostThankYouOptions;
		}

		public OneSpanSign.API.InpersonHostThankYouOptions ToAPIInpersonHostThankYouOptions()
		{
			if (sdkInpersonHostThankYouOptions == null)
			{
				return apiInpersonHostThankYouOptions;
			}

			OneSpanSign.API.InpersonHostThankYouOptions result = new OneSpanSign.API.InpersonHostThankYouOptions();

			result.Title = sdkInpersonHostThankYouOptions.Title;
			result.Body = sdkInpersonHostThankYouOptions.Body;
			result.RecipientName = sdkInpersonHostThankYouOptions.RecipientName;
			result.RecipientEmail = sdkInpersonHostThankYouOptions.RecipientEmail;
			result.RecipientRole = sdkInpersonHostThankYouOptions.RecipientRole;
			result.RecipientStatus = sdkInpersonHostThankYouOptions.RecipientStatus;
			result.DownloadButton = sdkInpersonHostThankYouOptions.DownloadButton;
			result.ReviewDocumentsButton = sdkInpersonHostThankYouOptions.ReviewDocumentsButton;

            return result;
		}

		public OneSpanSign.Sdk.InpersonHostThankYouOptions ToSDKInpersonHostThankYouOptions()
		{
			if (apiInpersonHostThankYouOptions == null)
			{
				return sdkInpersonHostThankYouOptions;
			}

			OneSpanSign.Sdk.InpersonHostThankYouOptions result = new OneSpanSign.Sdk.InpersonHostThankYouOptions();
			result.Title = apiInpersonHostThankYouOptions.Title.Value;
			result.Body = apiInpersonHostThankYouOptions.Body.Value;
			result.RecipientName = apiInpersonHostThankYouOptions.RecipientName.Value;
			result.RecipientEmail = apiInpersonHostThankYouOptions.RecipientEmail.Value;
			result.RecipientRole = apiInpersonHostThankYouOptions.RecipientRole.Value;
			result.RecipientStatus = apiInpersonHostThankYouOptions.RecipientStatus.Value;
			result.DownloadButton = apiInpersonHostThankYouOptions.DownloadButton.Value;
			result.ReviewDocumentsButton = apiInpersonHostThankYouOptions.ReviewDocumentsButton.Value;
			
            return result;
		}
		
    }
}

