using System;

namespace Silanis.ESL.SDK
{
	internal class RequirementStatusConverter
    {
		private Silanis.ESL.SDK.RequirementStatus sdkRequirementStatus;
		private Silanis.ESL.API.RequirementStatus apiRequirementStatus;

		/// <summary>
		/// Construct with API RequirementStatus object involved in conversion.
		/// </summary>
		/// <param name="apiRequirementStatus">API requirement status.</param>
		public RequirementStatusConverter(Silanis.ESL.API.RequirementStatus apiRequirementStatus)
		{
			this.apiRequirementStatus = apiRequirementStatus;
		}

		/// <summary>
		/// Construct with SDK RequirementStatus object involved in conversion.
		/// </summary>
		/// <param name="sdkRequirementStatus">SDK requirement status.</param>
		public RequirementStatusConverter(Silanis.ESL.SDK.RequirementStatus sdkRequirementStatus)
		{
			this.sdkRequirementStatus = sdkRequirementStatus;
		}

		/// <summary>
		/// Convert from SDK RequirementStatus to API RequirementStatus.
		/// </summary>
		/// <returns>The API requirement status.</returns>
		public Silanis.ESL.API.RequirementStatus ToAPIRequirementStatus()
		{
			switch (sdkRequirementStatus)
			{
				case Silanis.ESL.SDK.RequirementStatus.INCOMPLETE:
					return Silanis.ESL.API.RequirementStatus.INCOMPLETE;
				case Silanis.ESL.SDK.RequirementStatus.REJECTED:
					return Silanis.ESL.API.RequirementStatus.REJECTED;
				case Silanis.ESL.SDK.RequirementStatus.COMPLETE:
					return Silanis.ESL.API.RequirementStatus.COMPLETE;
				default:
                    throw new EslException(String.Format("Unable to decode the requirement status {0}", sdkRequirementStatus), null);
			}
		}

		/// <summary>
		/// Convert from API RequirementStatus to SDK RequirementStatus.
		/// </summary>
		/// <returns>The SDK requirement status.</returns>
		public Silanis.ESL.SDK.RequirementStatus ToSDKRequirementStatus()
		{
			switch (apiRequirementStatus)
			{
				case Silanis.ESL.API.RequirementStatus.INCOMPLETE:
					return Silanis.ESL.SDK.RequirementStatus.INCOMPLETE;
				case Silanis.ESL.API.RequirementStatus.REJECTED:
					return Silanis.ESL.SDK.RequirementStatus.REJECTED;
				case Silanis.ESL.API.RequirementStatus.COMPLETE:
					return Silanis.ESL.SDK.RequirementStatus.COMPLETE;
				default:
                    throw new EslException(String.Format("Unable to decode the requirement status {0}", apiRequirementStatus), null);
			}
		}
    }
}

