using System;

namespace Silanis.ESL.SDK
{
	internal class PackageStatusConverter
    {
		private Silanis.ESL.SDK.PackageStatus? sdkPackageStatus = null;
		private Silanis.ESL.API.PackageStatus? apiPackageStatus = null;

		/// <summary>
		/// Construct with API PackageStatus object involved in conversion.
		/// </summary>
		/// <param name="apiPackageStatus">API package status.</param>
		public PackageStatusConverter(Silanis.ESL.API.PackageStatus? apiPackageStatus)
		{
			this.apiPackageStatus = apiPackageStatus;
		}

		/// <summary>
		/// Construct with SDK PackageStatus object involved in conversion.
		/// </summary>
		/// <param name="sdkPackageStatus">SDK package status.</param>
		public PackageStatusConverter(Silanis.ESL.SDK.PackageStatus? sdkPackageStatus)
		{
			this.sdkPackageStatus = sdkPackageStatus;
		}

		/// <summary>
		/// Convert from SDK PackageStatus to API PackageStatus.
		/// </summary>
		/// <returns>The API package status.</returns>
		public Silanis.ESL.API.PackageStatus? ToAPIPackageStatus()
		{
			switch (sdkPackageStatus)
			{
				case PackageStatus.DRAFT:
					return Silanis.ESL.API.PackageStatus.DRAFT;
				case PackageStatus.SENT:
					return Silanis.ESL.API.PackageStatus.SENT;
				case PackageStatus.COMPLETED:
					return Silanis.ESL.API.PackageStatus.COMPLETED;
				case PackageStatus.ARCHIVED:
					return Silanis.ESL.API.PackageStatus.ARCHIVED;
				case PackageStatus.DECLINED:
					return Silanis.ESL.API.PackageStatus.DECLINED;
				case PackageStatus.OPTED_OUT:
					return Silanis.ESL.API.PackageStatus.OPTED_OUT;
				case PackageStatus.EXPIRED:
					return Silanis.ESL.API.PackageStatus.EXPIRED;
				default:
					break;
			}

			return apiPackageStatus;
		}

		/// <summary>
		/// Convert from API PackageStatus to SDK PackageStatus.
		/// </summary>
		/// <returns>The SDK package status.</returns>
		public Silanis.ESL.SDK.PackageStatus? ToSDKPackageStatus()
		{
			switch (apiPackageStatus)
			{
				case Silanis.ESL.API.PackageStatus.DRAFT:
					return PackageStatus.DRAFT;
				case Silanis.ESL.API.PackageStatus.SENT:
					return PackageStatus.SENT;
				case Silanis.ESL.API.PackageStatus.COMPLETED:
					return PackageStatus.COMPLETED;
				case Silanis.ESL.API.PackageStatus.ARCHIVED:
					return PackageStatus.ARCHIVED;
				case Silanis.ESL.API.PackageStatus.DECLINED:
					return PackageStatus.DECLINED;
				case Silanis.ESL.API.PackageStatus.OPTED_OUT:
					return PackageStatus.OPTED_OUT;
				case Silanis.ESL.API.PackageStatus.EXPIRED:
					return PackageStatus.EXPIRED;
				default:
					break;
			}

			return sdkPackageStatus;
		}
    }
}

