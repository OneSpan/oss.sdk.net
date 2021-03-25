using NUnit.Framework;
using OneSpanSign.API;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Internal.Conversion;

namespace SDK.Tests
{
    [TestFixture()]
    public class HandoverConverterTest
    {
        private OneSpanSign.API.Handover apiHandover1 = null;
        private OneSpanSign.API.Handover apiHandover2 = null;
        private OneSpanSign.Sdk.Handover sdkHandover1 = null;
        private OneSpanSign.Sdk.Handover sdkHandover2 = null;
        private HandoverConverter converter = null;
        private string lang = "en";

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkHandover1 = null;
            converter = new HandoverConverter(sdkHandover1);
            Assert.IsNull(converter.ToAPIHandover());
        }

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiHandover1 = null;
            converter = new HandoverConverter(apiHandover1);
            Assert.IsNull(converter.ToAPIHandover());
            Assert.IsNull(converter.ToSDKHandover("ko"));
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkHandover1 = null;
            converter = new HandoverConverter(sdkHandover1);
            Assert.IsNull(converter.ToSDKHandover("fr"));
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiHandover1 = null;
            converter = new HandoverConverter(apiHandover1);
            Assert.IsNull(converter.ToAPIHandover());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkHandover1 = CreateTypicalSDKHandover();
            sdkHandover2 = new HandoverConverter(sdkHandover1).ToSDKHandover(lang);

            Assert.IsNotNull(sdkHandover2);
            Assert.AreEqual(sdkHandover2.Href, sdkHandover1.Href);
            Assert.AreEqual(sdkHandover2.Text, sdkHandover1.Text);
            Assert.AreEqual(sdkHandover2.Title, sdkHandover1.Title);
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiHandover1 = CreateTypicalAPILink();
            apiHandover2 = new HandoverConverter(apiHandover1).ToAPIHandover();

            Assert.IsNotNull(apiHandover2);
            Assert.AreEqual(apiHandover2.Href, apiHandover1.Href);
            Assert.AreEqual(apiHandover2.Text, apiHandover1.Text);
            Assert.AreEqual(apiHandover2.Title, apiHandover1.Title);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiHandover1 = CreateTypicalAPILink();
            sdkHandover1 = new HandoverConverter(apiHandover1).ToSDKHandover(lang);

            Assert.IsNotNull(sdkHandover1);
            Assert.AreEqual(sdkHandover1.Language, lang);
            Assert.AreEqual(sdkHandover1.Href, apiHandover1.Href);
            Assert.AreEqual(sdkHandover1.Text, apiHandover1.Text);
            Assert.AreEqual(sdkHandover1.Title, apiHandover1.Title);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkHandover1 = CreateTypicalSDKHandover();
            apiHandover1 = new HandoverConverter(sdkHandover1).ToAPIHandover();

            Assert.IsNotNull(apiHandover1);
            Assert.AreEqual(apiHandover1.Href, sdkHandover1.Href);
            Assert.AreEqual(apiHandover1.Text, sdkHandover1.Text);
            Assert.AreEqual(apiHandover1.Title, sdkHandover1.Title);
        }

        private OneSpanSign.Sdk.Handover CreateTypicalSDKHandover()
        {
            return HandoverBuilder.NewHandover(lang)
                .WithHref("sdkHref")
                .WithTitle("sdkTitle")
                .WithText("sdkText")
                .Build();
        }

        private OneSpanSign.API.Handover CreateTypicalAPILink()
        {
            OneSpanSign.API.Handover apiHandover = new OneSpanSign.API.Handover();
            apiHandover.Href = "apiHref";
            apiHandover.Title = "apiTitle";
            apiHandover.Text = "apiText";
            return apiHandover;
        }
    }
}