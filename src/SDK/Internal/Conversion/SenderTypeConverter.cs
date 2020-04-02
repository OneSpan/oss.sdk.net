using System;

namespace OneSpanSign.Sdk
{
	internal class SenderTypeConverter
    {
		private OneSpanSign.Sdk.SenderType sdkSenderType;
		private string apiSenderType;

		/// <summary>
		/// Construct with API SenderType object involved in conversion.
		/// </summary>
		/// <param name="apiSenderType">API sender type.</param>
		public SenderTypeConverter(string apiSenderType)
		{
			this.apiSenderType = apiSenderType;
		}

		/// <summary>
		/// Construct with SDK SenderType object involved in conversion.
		/// </summary>
		/// <param name="sdkSenderType">SDK sender type.</param>
		public SenderTypeConverter(OneSpanSign.Sdk.SenderType sdkSenderType)
		{
			this.sdkSenderType = sdkSenderType;
		}

		/// <summary>
		/// Convert from SDK SenderType to API SenderType.
		/// </summary>
		/// <returns>The API sender type.</returns>
		public string ToAPISenderType()
		{
            return sdkSenderType.getApiValue();
		}

		/// <summary>
		/// Convert from API SenderType to SDK SenderType.
		/// </summary>
		/// <returns>The SDK sender type.</returns>
		public OneSpanSign.Sdk.SenderType ToSDKSenderType()
		{
            return OneSpanSign.Sdk.SenderType.valueOf(apiSenderType);
		}
    }
}

