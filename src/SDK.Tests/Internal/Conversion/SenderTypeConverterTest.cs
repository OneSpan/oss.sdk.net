using System;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
	[TestFixture]
    public class SenderTypeConverterTest
    {
		private OneSpanSign.Sdk.SenderType sdkSenderType1;
        private string apiSenderType1;

       
        [Test]
        public void ConvertAPIRegularToRegularSenderType()
        {
            apiSenderType1 = "REGULAR";
            sdkSenderType1 = new SenderTypeConverter(apiSenderType1).ToSDKSenderType();

            Assert.AreEqual(sdkSenderType1.getApiValue(), apiSenderType1);
        }

        [Test]
        public void ConvertAPIManagerToManagerSenderType()
        {
            apiSenderType1 = "MANAGER";
            sdkSenderType1 = new SenderTypeConverter(apiSenderType1).ToSDKSenderType();

            Assert.AreEqual(sdkSenderType1.getApiValue(), apiSenderType1);
        }

        [Test]
        public void ConvertAPIUnknonwnValueToUnrecognizedSenderType()
        {
            apiSenderType1 = "NEWLY_ADDED_SENDER_TYPE";
            sdkSenderType1 = new SenderTypeConverter(apiSenderType1).ToSDKSenderType();

            Assert.AreEqual(sdkSenderType1.getApiValue(), apiSenderType1);
        }

        [Test]
        public void ConvertSDKRegularToAPIRegular()
        {
            sdkSenderType1 = OneSpanSign.Sdk.SenderType.REGULAR;
            apiSenderType1 = new SenderTypeConverter(sdkSenderType1).ToAPISenderType();

            Assert.AreEqual("REGULAR", apiSenderType1);
        }

        [Test]
        public void ConvertSDKManagerToAPIManager()
        {
            sdkSenderType1 = OneSpanSign.Sdk.SenderType.MANAGER;
            apiSenderType1 = new SenderTypeConverter(sdkSenderType1).ToAPISenderType();

            Assert.AreEqual("MANAGER", apiSenderType1);
        }
       
        [Test]
        public void ConvertSDKUnrecognizedSenderTypeToAPIUnknownValue()
        {
            apiSenderType1 = "NEWLY_ADDED_SENDER_TYPE";
            SenderType unrecognizedSenderType = SenderType.valueOf(apiSenderType1);
            string acutalAPIValue = new SenderTypeConverter(unrecognizedSenderType).ToAPISenderType();

            Assert.AreEqual(apiSenderType1, acutalAPIValue);
        }
    }
}

