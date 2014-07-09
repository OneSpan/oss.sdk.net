using System;

namespace Silanis.ESL.SDK
{
	internal class SenderTypeConverter
    {
		private Silanis.ESL.SDK.SenderType sdkSenderType;
		private Silanis.ESL.API.SenderType apiSenderType;

		/// <summary>
		/// Construct with API SenderType object involved in conversion.
		/// </summary>
		/// <param name="apiSenderType">API sender type.</param>
		public SenderTypeConverter(Silanis.ESL.API.SenderType apiSenderType)
		{
			this.apiSenderType = apiSenderType;
		}

		/// <summary>
		/// Construct with SDK SenderType object involved in conversion.
		/// </summary>
		/// <param name="sdkSenderType">SDK sender type.</param>
		public SenderTypeConverter(Silanis.ESL.SDK.SenderType sdkSenderType)
		{
			this.sdkSenderType = sdkSenderType;
		}

		/// <summary>
		/// Convert from SDK SenderType to API SenderType.
		/// </summary>
		/// <returns>The API sender type.</returns>
		public Silanis.ESL.API.SenderType ToAPISenderType()
		{
			switch (sdkSenderType)
			{
				case SenderType.MANAGER:
					return Silanis.ESL.API.SenderType.MANAGER;
				case SenderType.REGULAR:
					return Silanis.ESL.API.SenderType.REGULAR;
				default:
                    throw new EslException(String.Format("Unable to decode the sender type {0}", sdkSenderType), null);
			}
		}

		/// <summary>
		/// Convert from API SenderType to SDK SenderType.
		/// </summary>
		/// <returns>The SDK sender type.</returns>
		public Silanis.ESL.SDK.SenderType ToSDKSenderType()
		{
			switch (apiSenderType)
			{
				case Silanis.ESL.API.SenderType.MANAGER:
					return SenderType.MANAGER;
				case Silanis.ESL.API.SenderType.REGULAR:
					return SenderType.REGULAR;
				default:
                    throw new EslException(String.Format("Unable to decode the sender type {0}", apiSenderType), null);
			}
		}
    }
}

