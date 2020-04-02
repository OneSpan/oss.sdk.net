using NUnit.Framework;
using System;
using OneSpanSign.API;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.src.Internal.Conversion;

namespace SDK.Tests
{
    [TestFixture()]
    public class SenderStatusConverterTest
    {
        private OneSpanSign.Sdk.SenderStatus sdkSenderStatus1;
        private string apiSenderStatus1;

        [Test]
        public void ConvertAPIACTIVEoACTIVESenderStatus()
        {
            apiSenderStatus1 = "ACTIVE";
            sdkSenderStatus1 = new SenderStatusConverter(apiSenderStatus1).ToSDKSenderStatus();

            Assert.AreEqual(apiSenderStatus1, sdkSenderStatus1.getApiValue());
        }

        [Test]
        public void ConvertAPIINVITEDToINVITEDSenderStatus()
        {
            apiSenderStatus1 = "INVITED";
            sdkSenderStatus1 = new SenderStatusConverter(apiSenderStatus1).ToSDKSenderStatus();

            Assert.AreEqual(apiSenderStatus1, sdkSenderStatus1.getApiValue());
        }

        [Test]
        public void ConvertAPILOCKEDToLOCKEDSenderStatus()
        {
            apiSenderStatus1 = "LOCKED";
            sdkSenderStatus1 = new SenderStatusConverter(apiSenderStatus1).ToSDKSenderStatus();

            Assert.AreEqual(apiSenderStatus1, sdkSenderStatus1.getApiValue());
        }

        [Test]
        public void ConvertAPIUnknonwnValueToUnrecognizedSenderStatus()
        {
            apiSenderStatus1 = "NEWLY_ADDED_SENDER_STATUS";
            sdkSenderStatus1 = new SenderStatusConverter(apiSenderStatus1).ToSDKSenderStatus();

            Assert.AreEqual(sdkSenderStatus1.getApiValue(), apiSenderStatus1);
        }

        [Test]
        public void ConvertSDKACTIVEToAPIACTIVE()
        {
            sdkSenderStatus1 = OneSpanSign.Sdk.SenderStatus.ACTIVE;
            apiSenderStatus1 = new SenderStatusConverter(sdkSenderStatus1).ToAPISenderStatus();

            Assert.AreEqual("ACTIVE", apiSenderStatus1);
        }

        [Test]
        public void ConvertSDKINVITEDToAPIINVITED()
        {
            sdkSenderStatus1 = OneSpanSign.Sdk.SenderStatus.INVITED;
            apiSenderStatus1 = new SenderStatusConverter(sdkSenderStatus1).ToAPISenderStatus();

            Assert.AreEqual("INVITED", apiSenderStatus1);
        }

        [Test]
        public void ConvertSDKLOCKEDToAPILOCKED()
        {
            sdkSenderStatus1 = OneSpanSign.Sdk.SenderStatus.LOCKED;
            apiSenderStatus1 = new SenderStatusConverter(sdkSenderStatus1).ToAPISenderStatus();

            Assert.AreEqual("LOCKED", apiSenderStatus1);
        }

        [Test]
        public void ConvertSDKUnrecognizedSenderStatusToAPIUnknownValue()
        {
            apiSenderStatus1 = "NEWLY_ADDED_SENDER_STATUS";
            SenderStatus unrecognizedSenderStatus = SenderStatus.valueOf(apiSenderStatus1);
            string acutalApiScheme = new SenderStatusConverter(unrecognizedSenderStatus).ToAPISenderStatus();

            Assert.AreEqual(apiSenderStatus1, acutalApiScheme);
        }

    }
}

