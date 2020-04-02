using System;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture()]
    public class MessageStatusConverterTest
    {
        private OneSpanSign.Sdk.MessageStatus sdkMessageStatus1;
        private string apiMessageStatus1;

        [Test]
        public void ConvertAPINewToSDKNew()
        {
            string expectedAPIValue = "NEW";
            sdkMessageStatus1 = new MessageStatusConverter(expectedAPIValue).ToSDKMessageStatus();

            Assert.AreEqual(expectedAPIValue, sdkMessageStatus1.getApiValue());
        }

        [Test]
        public void ConvertAPIReadToSDKRead()
        {
            string expectedAPIValue = "READ";
            sdkMessageStatus1 = new MessageStatusConverter(expectedAPIValue).ToSDKMessageStatus();

            Assert.AreEqual(expectedAPIValue, sdkMessageStatus1.getApiValue());
        }

        [Test]
        public void ConvertAPICHALLENGEToCHALLENGEMessageStatus()
        {
            string expectedAPIValue = "TRASHED";
            sdkMessageStatus1 = new MessageStatusConverter(expectedAPIValue).ToSDKMessageStatus();

            Assert.AreEqual(expectedAPIValue, sdkMessageStatus1.getApiValue());
        }

        [Test]
        public void ConvertAPIUnknonwnValueToUnrecognizedMessageStatus()
        {
            apiMessageStatus1 = "NEWLY_ADDED_MESSAGE_STATUS";
            sdkMessageStatus1 = new MessageStatusConverter(apiMessageStatus1).ToSDKMessageStatus();

            Assert.AreEqual(apiMessageStatus1, sdkMessageStatus1.getApiValue());
        }

        [Test]
        public void ConvertSDKNewToAPINew()
        {
            sdkMessageStatus1 = OneSpanSign.Sdk.MessageStatus.NEW;
            apiMessageStatus1 = new MessageStatusConverter(sdkMessageStatus1).ToAPIMessageStatus();

            Assert.AreEqual("NEW", apiMessageStatus1);
        }

        [Test]
        public void ConvertSDKReadToAPIRead()
        {
            sdkMessageStatus1 = OneSpanSign.Sdk.MessageStatus.READ;
            apiMessageStatus1 = new MessageStatusConverter(sdkMessageStatus1).ToAPIMessageStatus();

            Assert.AreEqual("READ", apiMessageStatus1);
        }

        [Test]
        public void ConvertSDKTrashedToAPITrashed()
        {
            sdkMessageStatus1 = OneSpanSign.Sdk.MessageStatus.TRASHED;
            apiMessageStatus1 = new MessageStatusConverter(sdkMessageStatus1).ToAPIMessageStatus();

            Assert.AreEqual("TRASHED", apiMessageStatus1);
        }

        [Test]
        public void ConvertSDKUnrecognizedMessageStatusToAPIUnknownValue()
        {
            apiMessageStatus1 = "NEWLY_ADDED_MESSAGE_STATUS";
            MessageStatus unrecognizedMessageStatus = MessageStatus.valueOf(apiMessageStatus1);
            string acutalAPIValue = new MessageStatusConverter(unrecognizedMessageStatus).ToAPIMessageStatus();

            Assert.AreEqual(apiMessageStatus1, acutalAPIValue);
        }

    }
}

