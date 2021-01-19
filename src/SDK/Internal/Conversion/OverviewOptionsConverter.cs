namespace OneSpanSign.Sdk
{
	internal class OverviewOptionsConverter
    {
		private OneSpanSign.Sdk.OverviewOptions sdkOverviewOptions;
		private OneSpanSign.API.OverviewOptions apiOverviewOptions;

		public OverviewOptionsConverter(OneSpanSign.API.OverviewOptions apiOverviewOptions)
        {
			this.apiOverviewOptions = apiOverviewOptions;
        }

		public OverviewOptionsConverter(OneSpanSign.Sdk.OverviewOptions sdkOverviewOptions)
		{
			this.sdkOverviewOptions = sdkOverviewOptions;
		}

		public OneSpanSign.API.OverviewOptions ToAPIOverviewOptions()
		{
			if (sdkOverviewOptions == null)
			{
				return apiOverviewOptions;
			}

			OneSpanSign.API.OverviewOptions result = new OneSpanSign.API.OverviewOptions();

			result.Title = sdkOverviewOptions.Title;
			result.Body = sdkOverviewOptions.Body;
			result.DocumentSection = sdkOverviewOptions.DocumentSection;
			result.UploadSection = sdkOverviewOptions.UploadSection;

			return result;
		}

		public OneSpanSign.Sdk.OverviewOptions ToSDKOverviewOptions()
		{
			if (apiOverviewOptions == null)
			{
				return sdkOverviewOptions;
			}

			OneSpanSign.Sdk.OverviewOptions result = new OneSpanSign.Sdk.OverviewOptions();
			result.Title = apiOverviewOptions.Title.Value;
			result.Body = apiOverviewOptions.Body.Value;
			result.DocumentSection = apiOverviewOptions.DocumentSection.Value;
			result.UploadSection = apiOverviewOptions.UploadSection.Value;

			return result;
		}
		
    }
}

