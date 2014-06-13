using System;

namespace Silanis.ESL.SDK
{
	internal class PackageStatusConverter
    {
		private Silanis.ESL.SDK.DocumentPackageStatus? sdkPackageStatus = null;
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
		public PackageStatusConverter(Silanis.ESL.SDK.DocumentPackageStatus? sdkPackageStatus)
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
				case DocumentPackageStatus.DRAFT:
					return Silanis.ESL.API.PackageStatus.DRAFT;
				case DocumentPackageStatus.SENT:
					return Silanis.ESL.API.PackageStatus.SENT;
				case DocumentPackageStatus.COMPLETED:
					return Silanis.ESL.API.PackageStatus.COMPLETED;
				case DocumentPackageStatus.ARCHIVED:
					return Silanis.ESL.API.PackageStatus.ARCHIVED;
				case DocumentPackageStatus.DECLINED:
					return Silanis.ESL.API.PackageStatus.DECLINED;
				case DocumentPackageStatus.OPTED_OUT:
					return Silanis.ESL.API.PackageStatus.OPTED_OUT;
				case DocumentPackageStatus.EXPIRED:
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
		public Silanis.ESL.SDK.DocumentPackageStatus? ToSDKPackageStatus()
		{
			switch (apiPackageStatus)
			{
				case Silanis.ESL.API.PackageStatus.DRAFT:
					return DocumentPackageStatus.DRAFT;
				case Silanis.ESL.API.PackageStatus.SENT:
					return DocumentPackageStatus.SENT;
				case Silanis.ESL.API.PackageStatus.COMPLETED:
					return DocumentPackageStatus.COMPLETED;
				case Silanis.ESL.API.PackageStatus.ARCHIVED:
					return DocumentPackageStatus.ARCHIVED;
				case Silanis.ESL.API.PackageStatus.DECLINED:
					return DocumentPackageStatus.DECLINED;
				case Silanis.ESL.API.PackageStatus.OPTED_OUT:
					return DocumentPackageStatus.OPTED_OUT;
				case Silanis.ESL.API.PackageStatus.EXPIRED:
					return DocumentPackageStatus.EXPIRED;
				default:
					break;
			}

			return sdkPackageStatus;
		}
    }
}

