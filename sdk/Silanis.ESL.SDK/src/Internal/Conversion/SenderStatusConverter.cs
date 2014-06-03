using System;

namespace Silanis.ESL.SDK
{
	internal class SenderStatusConverter
    {
		private Silanis.ESL.SDK.SenderStatus? sdkSenderStatus = null;
		private Silanis.ESL.API.SenderStatus? apiSenderStatus = null;

		/// <summary>
		/// Construct with API SenderStatus object involved in conversion.
		/// </summary>
		/// <param name="apiSenderStatus">API sender status.</param>
		public SenderStatusConverter(Silanis.ESL.API.SenderStatus? apiSenderStatus)
		{
			this.apiSenderStatus = apiSenderStatus;
		}

		/// <summary>
		/// Construct with SDK SenderStatus object involved in conversion.
		/// </summary>
		/// <param name="sdkSenderStatus">SDK sender status.</param>
		public SenderStatusConverter(Silanis.ESL.SDK.SenderStatus? sdkSenderStatus)
		{
			this.sdkSenderStatus = sdkSenderStatus;
		}

		/// <summary>
		/// Convert from SDK SenderStatus to API SenderStatus.
		/// </summary>
		/// <returns>The API sender status.</returns>
		public Silanis.ESL.API.SenderStatus? ToAPISenderStatus()
		{
			switch (sdkSenderStatus)
			{
				case SenderStatus.ACTIVE:
					return Silanis.ESL.API.SenderStatus.ACTIVE;
				case SenderStatus.INVITED:
					return Silanis.ESL.API.SenderStatus.INVITED;
				case SenderStatus.LOCKED:
					return Silanis.ESL.API.SenderStatus.LOCKED;
				default:
					break;
			}

			return apiSenderStatus;
		}

		/// <summary>
		/// Convert from API SenderStatus to SDK SenderStatus.
		/// </summary>
		/// <returns>The SDK sender status.</returns>
		public Silanis.ESL.SDK.SenderStatus? ToSDKSenderStatus()
		{
			switch (apiSenderStatus)
			{
				case Silanis.ESL.API.SenderStatus.ACTIVE:
					return SenderStatus.ACTIVE;
				case Silanis.ESL.API.SenderStatus.INVITED:
					return SenderStatus.INVITED;
				case Silanis.ESL.API.SenderStatus.LOCKED:
					return SenderStatus.LOCKED;
				default:
					break;
			}

			return sdkSenderStatus;
		}
    }
}

