using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
	[TestFixture]
    public class SenderStatusConverterTest
    {
		private Silanis.ESL.SDK.SenderStatus? sdkSenderStatus1 = null;
		private Silanis.ESL.SDK.SenderStatus? sdkSenderStatus2 = null;
		private Silanis.ESL.API.SenderStatus? apiSenderStatus1 = null;
		private Silanis.ESL.API.SenderStatus? apiSenderStatus2 = null;
		private SenderStatusConverter converter;

		[Test]
		public void ConvertNullSDKToAPI()
		{
			sdkSenderStatus1 = null;
			converter = new SenderStatusConverter(sdkSenderStatus1);
			Assert.IsNull(converter.ToAPISenderStatus());
		}

		[Test]
		public void ConvertNullAPIToSDK()
		{
			apiSenderStatus1 = null;
			converter = new SenderStatusConverter(apiSenderStatus1);
			Assert.IsNull(converter.ToSDKSenderStatus());
		}

		[Test]
		public void ConvertNullSDKToSDK()
		{
			sdkSenderStatus1 = null;
			converter = new SenderStatusConverter(sdkSenderStatus1);
			Assert.IsNull(converter.ToSDKSenderStatus());
		}

		[Test]
		public void ConvertNullAPIToAPI()
		{
			apiSenderStatus1 = null;
			converter = new SenderStatusConverter(apiSenderStatus1);
			Assert.IsNull(converter.ToAPISenderStatus());
		}

		[Test]
		public void ConvertSDKToSDK()
		{
			sdkSenderStatus1 = CreateTypicalSDKSenderStatus();
			sdkSenderStatus2 = new SenderStatusConverter(sdkSenderStatus1).ToSDKSenderStatus();

			Assert.IsNotNull(sdkSenderStatus2);
			Assert.AreEqual(sdkSenderStatus2, sdkSenderStatus1);
		}

		[Test]
		public void ConvertAPIToAPI()
		{
			apiSenderStatus1 = CreateTypicalAPISenderStatus();
			apiSenderStatus2 = new SenderStatusConverter(apiSenderStatus1).ToAPISenderStatus();

			Assert.IsNotNull(apiSenderStatus2);
			Assert.AreEqual(apiSenderStatus2, apiSenderStatus1);
		}

		[Test]
		public void ConvertAPIToSDK()
		{
			apiSenderStatus1 = CreateTypicalAPISenderStatus();
			sdkSenderStatus1 = new SenderStatusConverter(apiSenderStatus1).ToSDKSenderStatus();

			Assert.AreEqual(sdkSenderStatus1.ToString(), apiSenderStatus1.ToString());
		}

		[Test]
		public void ConvertSDKToAPI()
		{
			sdkSenderStatus1 = CreateTypicalSDKSenderStatus();
			apiSenderStatus1 = new SenderStatusConverter(sdkSenderStatus1).ToAPISenderStatus();

			Assert.AreEqual(apiSenderStatus1.ToString(), sdkSenderStatus1.ToString());
        }

		private Silanis.ESL.SDK.SenderStatus CreateTypicalSDKSenderStatus()
		{
			return SenderStatus.ACTIVE;
		}

		private Silanis.ESL.API.SenderStatus CreateTypicalAPISenderStatus()
		{
			return Silanis.ESL.API.SenderStatus.ACTIVE;
		}

    }
}

