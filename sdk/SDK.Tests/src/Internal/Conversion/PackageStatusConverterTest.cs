using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
	[TestFixture]
	public class PackageStatusConverterTest
	{
		private Silanis.ESL.SDK.DocumentPackageStatus? sdkPackageStatus1 = null;
		private Silanis.ESL.SDK.DocumentPackageStatus? sdkPackageStatus2 = null;
		private Silanis.ESL.API.PackageStatus? apiPackageStatus1 = null;
		private Silanis.ESL.API.PackageStatus? apiPackageStatus2 = null;
		private PackageStatusConverter converter;

		[Test]
		public void ConvertNullSDKToAPI()
		{
			sdkPackageStatus1 = null;
			converter = new PackageStatusConverter(sdkPackageStatus1);
			Assert.IsNull(converter.ToAPIPackageStatus());
		}

		[Test]
		public void ConvertNullAPIToSDK()
		{
			apiPackageStatus1 = null;
			converter = new PackageStatusConverter(apiPackageStatus1);
			Assert.IsNull(converter.ToSDKPackageStatus());
		}

		[Test]
		public void ConvertNullSDKToSDK()
		{
			sdkPackageStatus1 = null;
			converter = new PackageStatusConverter(sdkPackageStatus1);
			Assert.IsNull(converter.ToSDKPackageStatus());
		}

		[Test]
		public void ConvertNullAPIToAPI()
		{
			apiPackageStatus1 = null;
			converter = new PackageStatusConverter(apiPackageStatus1);
			Assert.IsNull(converter.ToAPIPackageStatus());
		}

		[Test]
		public void ConvertSDKToSDK()
		{
			sdkPackageStatus1 = CreateTypicalSDKPackageStatus();
			sdkPackageStatus2 = new PackageStatusConverter(sdkPackageStatus1).ToSDKPackageStatus();

			Assert.IsNotNull(sdkPackageStatus2);
			Assert.AreEqual(sdkPackageStatus2, sdkPackageStatus1);
		}

		[Test]
		public void ConvertAPIToAPI()
		{
			apiPackageStatus1 = CreateTypicalAPIPackageStatus();
			apiPackageStatus2 = new PackageStatusConverter(apiPackageStatus1).ToAPIPackageStatus();

			Assert.IsNotNull(apiPackageStatus2);
			Assert.AreEqual(apiPackageStatus2, apiPackageStatus1);
		}

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

