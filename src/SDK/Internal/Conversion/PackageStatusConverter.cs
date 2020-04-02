using System;

namespace OneSpanSign.Sdk
{
	internal class PackageStatusConverter
    {
		private OneSpanSign.Sdk.DocumentPackageStatus sdkPackageStatus;
		private string apiPackageStatus;

		/// <summary>
		/// Construct with API PackageStatus object involved in conversion.
		/// </summary>
		/// <param name="apiPackageStatus">API package status.</param>
		public PackageStatusConverter(string apiPackageStatus)
		{
			this.apiPackageStatus = apiPackageStatus;
		}

		/// <summary>
		/// Construct with SDK PackageStatus object involved in conversion.
		/// </summary>
		/// <param name="sdkPackageStatus">SDK package status.</param>
		public PackageStatusConverter(OneSpanSign.Sdk.DocumentPackageStatus sdkPackageStatus)
		{
			this.sdkPackageStatus = sdkPackageStatus;
		}

		/// <summary>
		/// Convert from SDK PackageStatus to API PackageStatus.
		/// </summary>
		/// <returns>The API package status.</returns>
		public string ToAPIPackageStatus()
		{
            return sdkPackageStatus.getApiValue();			
		}

		/// <summary>
		/// Convert from API PackageStatus to SDK PackageStatus.
		/// </summary>
		/// <returns>The SDK package status.</returns>
		public OneSpanSign.Sdk.DocumentPackageStatus ToSDKPackageStatus()
		{
            return OneSpanSign.Sdk.DocumentPackageStatus.valueOf(apiPackageStatus);
		}
    }
}

