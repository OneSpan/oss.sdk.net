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
        private Link apiLink1 = null;
        private Link apiLink2 = null;
        private Handover sdkHandover1 = null;
        private Handover sdkHandover2 = null;
        private HandoverConverter converter = null;
        private string lang = "en";

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkHandover1 = null;
            converter = new HandoverConverter(sdkHandover1);
            Assert.IsNull(converter.ToAPILink());
        }

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiLink1 = null;
            converter = new HandoverConverter(apiLink1);
            Assert.IsNull(converter.ToAPILink());
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
            apiLink1 = null;
            converter = new HandoverConverter(apiLink1);
            Assert.IsNull(converter.ToAPILink());
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
            apiLink1 = CreateTypicalAPILink();
            apiLink2 = new HandoverConverter(apiLink1).ToAPILink();

            Assert.IsNotNull(apiLink2);
            Assert.AreEqual(apiLink2.Href, apiLink1.Href);
            Assert.AreEqual(apiLink2.Text, apiLink1.Text);
            Assert.AreEqual(apiLink2.Title, apiLink1.Title);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiLink1 = CreateTypicalAPILink();
            sdkHandover1 = new HandoverConverter(apiLink1).ToSDKHandover(lang);

            Assert.IsNotNull(sdkHandover1);
            Assert.AreEqual(sdkHandover1.Language, lang);
            Assert.AreEqual(sdkHandover1.Href, apiLink1.Href);
            Assert.AreEqual(sdkHandover1.Text, apiLink1.Text);
            Assert.AreEqual(sdkHandover1.Title, apiLink1.Title);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkHandover1 = CreateTypicalSDKHandover();
            apiLink1 = new HandoverConverter(sdkHandover1).ToAPILink();

            Assert.IsNotNull(apiLink1);
            Assert.AreEqual(apiLink1.Href, sdkHandover1.Href);
            Assert.AreEqual(apiLink1.Text, sdkHandover1.Text);
            Assert.AreEqual(apiLink1.Title, sdkHandover1.Title);
        }

        private Handover CreateTypicalSDKHandover()
        {
            return HandoverBuilder.NewHandover(lang)
                .WithHref("sdkHref")
                .WithTitle("sdkTitle")
                .WithText("sdkText")
                .Build();
        }

        private Link CreateTypicalAPILink()
        {
            Link link = new Link();
            link.Href = "apiHref";
            link.Title = "apiTitle";
            link.Text = "apiText";
            return link;
        }
    }
}