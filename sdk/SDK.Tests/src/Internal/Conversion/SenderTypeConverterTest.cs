using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
	[TestFixture]
    public class SenderTypeConverterTest
    {
		private Silanis.ESL.SDK.SenderType sdkSenderType1;
        private Silanis.ESL.API.SenderType apiSenderType1;

		[Test]
		public void ConvertAPIToSDK()
		{
			apiSenderType1 = CreateTypicalAPISenderType();
			sdkSenderType1 = new SenderTypeConverter(apiSenderType1).ToSDKSenderType();

			Assert.AreEqual(sdkSenderType1.ToString(), apiSenderType1.ToString());
		}

		[Test]
		public void ConvertSDKToAPI()
		{
			sdkSenderType1 = CreateTypicalSDKSenderType();
			apiSenderType1 = new SenderTypeConverter(sdkSenderType1).ToAPISenderType();

			Assert.AreEqual(apiSenderType1.ToString(), sdkSenderType1.ToString());
		}

		private Silanis.ESL.SDK.SenderType CreateTypicalSDKSenderType()
		{
			return SenderType.MANAGER;
		}

		private Silanis.ESL.API.SenderType CreateTypicalAPISenderType()
		{
			return Silanis.ESL.API.SenderType.REGULAR;
		}
    }
}

