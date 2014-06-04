using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
	[TestFixture]
    public class SenderTypeConverterTest
    {
		private Silanis.ESL.SDK.SenderType? sdkSenderType1 = null;
		private Silanis.ESL.SDK.SenderType? sdkSenderType2 = null;
		private Silanis.ESL.API.SenderType? apiSenderType1 = null;
		private Silanis.ESL.API.SenderType? apiSenderType2 = null;
		private SenderTypeConverter converter;

		[Test]
		public void ConvertNullSDKToAPI()
		{
			sdkSenderType1 = null;
			converter = new SenderTypeConverter(sdkSenderType1);
			Assert.IsNull(converter.ToAPISenderType());
		}

		[Test]
		public void ConvertNullAPIToSDK()
		{
			apiSenderType1 = null;
			converter = new SenderTypeConverter(apiSenderType1);
			Assert.IsNull(converter.ToSDKSenderType());
		}

		[Test]
		public void ConvertNullSDKToSDK()
		{
			sdkSenderType1 = null;
			converter = new SenderTypeConverter(sdkSenderType1);
			Assert.IsNull(converter.ToSDKSenderType());
		}

		[Test]
		public void ConvertNullAPIToAPI()
		{
			apiSenderType1 = null;
			converter = new SenderTypeConverter(apiSenderType1);
			Assert.IsNull(converter.ToAPISenderType());
		}

		[Test]
		public void ConvertSDKToSDK()
		{
			sdkSenderType1 = CreateTypicalSDKSenderType();
			sdkSenderType2 = new SenderTypeConverter(sdkSenderType1).ToSDKSenderType();

			Assert.IsNotNull(sdkSenderType2);
			Assert.AreEqual(sdkSenderType2, sdkSenderType1);
		}

		[Test]
		public void ConvertAPIToAPI()
		{
			apiSenderType1 = CreateTypicalAPISenderType();
			apiSenderType2 = new SenderTypeConverter(apiSenderType1).ToAPISenderType();

			Assert.IsNotNull(apiSenderType2);
			Assert.AreEqual(apiSenderType2, apiSenderType1);
		}

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

