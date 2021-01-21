namespace OneSpanSign.Sdk
{
	internal class CompleteSummaryOptionsConverter
    {
		private OneSpanSign.Sdk.CompleteSummaryOptions sdkCompleteSummaryOptions;
		private OneSpanSign.API.CompleteSummaryOptions apiCompleteSummaryOptions;

		public CompleteSummaryOptionsConverter(OneSpanSign.API.CompleteSummaryOptions apiCompleteSummaryOptions)
        {
			this.apiCompleteSummaryOptions = apiCompleteSummaryOptions;
        }

		public CompleteSummaryOptionsConverter(OneSpanSign.Sdk.CompleteSummaryOptions sdkCompleteSummaryOptions)
		{
			this.sdkCompleteSummaryOptions = sdkCompleteSummaryOptions;
		}

		public OneSpanSign.API.CompleteSummaryOptions ToAPICompleteSummaryOptions()
		{
			if (sdkCompleteSummaryOptions == null)
			{
				return apiCompleteSummaryOptions;
			}

			OneSpanSign.API.CompleteSummaryOptions result = new OneSpanSign.API.CompleteSummaryOptions();

			result.From = sdkCompleteSummaryOptions.From;
			result.Title = sdkCompleteSummaryOptions.Title;
			result.Message = sdkCompleteSummaryOptions.Message;
			result.Download = sdkCompleteSummaryOptions.Download;
			result.Review = sdkCompleteSummaryOptions.Review;
			result.Continue = sdkCompleteSummaryOptions.Continue;

            return result;
		}

		public OneSpanSign.Sdk.CompleteSummaryOptions ToSDKCompleteSummaryOptions()
		{
			if (apiCompleteSummaryOptions == null)
			{
				return sdkCompleteSummaryOptions;
			}

			OneSpanSign.Sdk.CompleteSummaryOptions result = new OneSpanSign.Sdk.CompleteSummaryOptions();
			result.From = apiCompleteSummaryOptions.From.Value;
			result.Title = apiCompleteSummaryOptions.Title.Value;
			result.Message = apiCompleteSummaryOptions.Message.Value;
			result.Download = apiCompleteSummaryOptions.Download.Value;
			result.Review = apiCompleteSummaryOptions.Review.Value;
			result.Continue = apiCompleteSummaryOptions.Continue.Value;

            return result;
		}
		
    }
}

