using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
    [TestFixture()]
    public class MessageStatusConverterTest
    {
        private Silanis.ESL.SDK.MessageStatus sdkMessageStatus1;
        private Silanis.ESL.API.MessageStatus apiMessageStatus1;

        [Test]
        public void ConvertAPIToSDK()
        {
            apiMessageStatus1 = CreateTypicalAPIMessageStatus();
            sdkMessageStatus1 = new MessageStatusConverter(apiMessageStatus1).ToSDKMessageStatus();

            Assert.AreEqual(sdkMessageStatus1.ToString(), apiMessageStatus1.ToString());
        }

        [Test]
        public void ConvertSDKToAPI()
        {
            sdkMessageStatus1 = CreateTypicalSDKMessageStatus();
            apiMessageStatus1 = new MessageStatusConverter(sdkMessageStatus1).ToAPIMessageStatus();

            Assert.AreEqual(apiMessageStatus1.ToString(), sdkMessageStatus1.ToString());
        }

        private Silanis.ESL.SDK.MessageStatus CreateTypicalSDKMessageStatus()
        {
            return MessageStatus.NEW;
        }

        private Silanis.ESL.API.MessageStatus CreateTypicalAPIMessageStatus()
        {
            return Silanis.ESL.API.MessageStatus.READ;
        }
    }
}

