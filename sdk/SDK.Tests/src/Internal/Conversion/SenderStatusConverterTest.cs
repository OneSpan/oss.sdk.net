using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
	[TestFixture]
    public class SenderStatusConverterTest
    {
		private Silanis.ESL.SDK.SenderStatus sdkSenderStatus1;
        private Silanis.ESL.API.SenderStatus apiSenderStatus1;

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

