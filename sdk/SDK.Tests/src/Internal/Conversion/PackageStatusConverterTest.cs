using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
	[TestFixture]
	public class PackageStatusConverterTest
	{
		private Silanis.ESL.SDK.DocumentPackageStatus sdkPackageStatus1;
		private Silanis.ESL.API.PackageStatus apiPackageStatus1;

		[Test]
		public void ConvertAPIToSDK()
		{
			apiPackageStatus1 = CreateTypicalAPIPackageStatus();
			sdkPackageStatus1 = new PackageStatusConverter(apiPackageStatus1).ToSDKPackageStatus();

			Assert.AreEqual(sdkPackageStatus1.ToString(), apiPackageStatus1.ToString());
		}

		[Test]
		public void ConvertSDKToAPI()
		{
			sdkPackageStatus1 = CreateTypicalSDKPackageStatus();
			apiPackageStatus1 = new PackageStatusConverter(sdkPackageStatus1).ToAPIPackageStatus();

			Assert.AreEqual(apiPackageStatus1.ToString(), sdkPackageStatus1.ToString());
		}

		private Silanis.ESL.SDK.DocumentPackageStatus CreateTypicalSDKPackageStatus()
		{
			return DocumentPackageStatus.DRAFT;
		}

		private Silanis.ESL.API.PackageStatus CreateTypicalAPIPackageStatus()
		{
			return Silanis.ESL.API.PackageStatus.EXPIRED;
		}

	}
}

