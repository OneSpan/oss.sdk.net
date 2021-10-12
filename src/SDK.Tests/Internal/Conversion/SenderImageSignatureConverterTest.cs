using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
namespace SDK.Tests
{
    [TestFixture()]
    public class SenderImageSignatureConverterTest
    {

        private OneSpanSign.Sdk.SenderImageSignature sdkSenderImageSignature1 = null;
        private OneSpanSign.Sdk.SenderImageSignature sdkSenderImageSignature2 = null;
        private OneSpanSign.API.SenderImageSignature apiSenderImageSignature1 = null;
        private OneSpanSign.API.SenderImageSignature apiSenderImageSignature2 = null;
        private SenderImageSignatureConverter converter = null;

        [Test()]
        public void convertNullSDKToAPI()
        {
            sdkSenderImageSignature1 = null;
            converter = new SenderImageSignatureConverter(sdkSenderImageSignature1);
            Assert.IsNull(converter.ToAPISenderImageSignature());

        }

        [Test()]
        public void convertNullAPIToSDK()
        {
            apiSenderImageSignature1 = null;
            converter = new SenderImageSignatureConverter(apiSenderImageSignature1);
            Assert.IsNull(converter.ToSDKSenderImageSignature());

        }

        [Test()]
        public void convertNullSDKToSDK()
        {
            sdkSenderImageSignature1 = null;
            converter = new SenderImageSignatureConverter(sdkSenderImageSignature1);
            Assert.IsNull(converter.ToSDKSenderImageSignature());

        }

        [Test()]
        public void convertNullAPIToAPI()
        {
            apiSenderImageSignature1 = null;
            converter = new SenderImageSignatureConverter(apiSenderImageSignature1);
            Assert.IsNull(converter.ToAPISenderImageSignature());

        }

        [Test()]
        public void convertSDKToSDK()
        {
            sdkSenderImageSignature1 = new SenderImageSignature();
            sdkSenderImageSignature2 = new SenderImageSignatureConverter(sdkSenderImageSignature1).ToSDKSenderImageSignature();
            Assert.IsNotNull(sdkSenderImageSignature2);
            Assert.AreEqual(sdkSenderImageSignature2, sdkSenderImageSignature1);
        }

        [Test()]
        public void convertAPIToAPI()
        {
            apiSenderImageSignature1 = new OneSpanSign.API.SenderImageSignature();
            apiSenderImageSignature2 = new SenderImageSignatureConverter(apiSenderImageSignature1).ToAPISenderImageSignature();

            Assert.IsNotNull(apiSenderImageSignature2);
            Assert.AreEqual(apiSenderImageSignature2, apiSenderImageSignature1);

        }

        [Test()]
        public void convertAPIToSDK()
        {
            apiSenderImageSignature1 = buildApiSenderImageSignature();
            sdkSenderImageSignature1 = new SenderImageSignatureConverter(apiSenderImageSignature1).ToSDKSenderImageSignature();

            Assert.IsNotNull(sdkSenderImageSignature1);
            Assert.AreEqual(sdkSenderImageSignature1.Content, apiSenderImageSignature1.Content);
            Assert.AreEqual(sdkSenderImageSignature1.FileName, apiSenderImageSignature1.FileName);
            Assert.AreEqual(sdkSenderImageSignature1.MediaType, apiSenderImageSignature1.MediaType);
        }

        [Test()]
        public void convertSDKToAPI()
        {
            sdkSenderImageSignature1 = buildSdkSenderImageSignature();
            apiSenderImageSignature1 = new SenderImageSignatureConverter(sdkSenderImageSignature1).ToAPISenderImageSignature();

            Assert.IsNotNull(apiSenderImageSignature1);
            Assert.AreEqual(apiSenderImageSignature1.Content, sdkSenderImageSignature1.Content);
            Assert.AreEqual(apiSenderImageSignature1.FileName, sdkSenderImageSignature1.FileName);
            Assert.AreEqual(apiSenderImageSignature1.MediaType, sdkSenderImageSignature1.MediaType);
        }

        private SenderImageSignature buildSdkSenderImageSignature()
        {
            SenderImageSignature result = new SenderImageSignature();
            result.Content = "content";
            result.FileName = "fileName";
            result.MediaType = "mediaType";
            return result;
        }


        private OneSpanSign.API.SenderImageSignature buildApiSenderImageSignature()
        {
            OneSpanSign.API.SenderImageSignature result = new OneSpanSign.API.SenderImageSignature();
            result.Content = "content";
            result.FileName = "fileName";
            result.MediaType = "mediaType";
            return result;
        }
    }
}
