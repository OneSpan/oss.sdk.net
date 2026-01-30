namespace OneSpanSign.Sdk
{
	internal class ChooseSignatureOptionsConverter
    {
		private OneSpanSign.Sdk.ChooseSignatureOptions sdkChooseSignatureOptions;
		private OneSpanSign.API.ChooseSignatureOptions apiChooseSignatureOptions;

		public ChooseSignatureOptionsConverter(OneSpanSign.API.ChooseSignatureOptions apiChooseSignatureOptions)
        {
			this.apiChooseSignatureOptions = apiChooseSignatureOptions;
        }

		public ChooseSignatureOptionsConverter(OneSpanSign.Sdk.ChooseSignatureOptions sdkChooseSignatureOptions)
		{
			this.sdkChooseSignatureOptions = sdkChooseSignatureOptions;
		}

		public OneSpanSign.API.ChooseSignatureOptions ToAPIChooseSignatureOptions()
		{
			if (sdkChooseSignatureOptions == null)
			{
				return apiChooseSignatureOptions;
			}

			OneSpanSign.API.ChooseSignatureOptions result = new OneSpanSign.API.ChooseSignatureOptions();

			result.AllowStyling = sdkChooseSignatureOptions.AllowStyling;
			result.AllowDrawing = sdkChooseSignatureOptions.AllowDrawing;
			result.AllowUploading = sdkChooseSignatureOptions.AllowUploading;
			result.AllowMobileSigning = sdkChooseSignatureOptions.AllowMobileSigning;
			//todo ChooseSignatureStyleType and fontsPerWritingSystem
            return result;
		}

		public OneSpanSign.Sdk.ChooseSignatureOptions ToSDKChooseSignatureOptions()
		{
			if (apiChooseSignatureOptions == null)
			{
				return sdkChooseSignatureOptions;
			}

			OneSpanSign.Sdk.ChooseSignatureOptions result = new OneSpanSign.Sdk.ChooseSignatureOptions();
			result.AllowStyling = apiChooseSignatureOptions.AllowStyling.Value;
			result.AllowDrawing = apiChooseSignatureOptions.AllowDrawing.Value;
			result.AllowUploading = apiChooseSignatureOptions.AllowUploading.Value;
			result.AllowMobileSigning = apiChooseSignatureOptions.AllowMobileSigning.Value;
			//todo ChooseSignatureStyleType and fontsPerWritingSystem

			return result;
		}
		
    }
}

