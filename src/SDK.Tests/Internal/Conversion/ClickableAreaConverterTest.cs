using System;
using NUnit.Framework;
using OneSpanSign.API;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.src.Internal.Conversion;

namespace SDK.Tests
{
    [TestFixture()]
    public class ClickableAreaConverterTest
    {
        private OneSpanSign.Sdk.ClickableArea sdkClickableArea1 = null;
        private OneSpanSign.Sdk.ClickableArea sdkClickableArea2 = null;
        private OneSpanSign.API.FieldClickableArea apiClickableArea1 = null;
        private OneSpanSign.API.FieldClickableArea apiClickableArea2 = null;

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkClickableArea1 = null;
            ClickableAreaConverter converter = new ClickableAreaConverter(sdkClickableArea1);
            Assert.IsNull(converter.ToAPIFieldClickableArea());
        }

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiClickableArea1 = null;
            ClickableAreaConverter converter = new ClickableAreaConverter(apiClickableArea1);
            Assert.IsNull(converter.ToSDKClickableArea());
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkClickableArea1 = null;
            ClickableAreaConverter converter = new ClickableAreaConverter(sdkClickableArea1);
            Assert.IsNull(converter.ToSDKClickableArea());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiClickableArea1 = null;
            ClickableAreaConverter converter = new ClickableAreaConverter(apiClickableArea1);
            Assert.IsNull(converter.ToAPIFieldClickableArea());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkClickableArea1 = CreateTypicalSDKClickableArea();
            sdkClickableArea2 = new ClickableAreaConverter(sdkClickableArea1).ToSDKClickableArea();
            Assert.IsNotNull(sdkClickableArea2);
            Assert.AreEqual(sdkClickableArea1, sdkClickableArea2);
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiClickableArea1 = CreateTypicalAPIClickableArea();
            apiClickableArea2 = new ClickableAreaConverter(apiClickableArea1).ToAPIFieldClickableArea();
            Assert.IsNotNull(apiClickableArea2);
            Assert.AreEqual(apiClickableArea1, apiClickableArea2);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiClickableArea1 = CreateTypicalAPIClickableArea();
            sdkClickableArea1 = new ClickableAreaConverter(apiClickableArea1).ToSDKClickableArea();
            Assert.IsNotNull(sdkClickableArea1);
            CompareClickableAreas(sdkClickableArea1, apiClickableArea1);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkClickableArea1 = CreateTypicalSDKClickableArea();
            apiClickableArea1 = new ClickableAreaConverter(sdkClickableArea1).ToAPIFieldClickableArea();
            Assert.IsNotNull(apiClickableArea1);
            CompareClickableAreas(sdkClickableArea1, apiClickableArea1);
        }

        private OneSpanSign.Sdk.ClickableArea CreateTypicalSDKClickableArea()
        {
            return ClickableAreaBuilder.NewClickableArea()
                .WithSize(20, 10)
                .WithAlignment(ClickableAreaAlignment.TOP_LEFT)
                .Build();
        }

        private OneSpanSign.API.FieldClickableArea CreateTypicalAPIClickableArea()
        {
            OneSpanSign.API.FieldClickableArea apiClickableArea = new OneSpanSign.API.FieldClickableArea();

            apiClickableArea.Width = 20.0;
            apiClickableArea.Height = 10.0;
            apiClickableArea.Alignment = "TOP_LEFT";

            return apiClickableArea;
        }

        private void CompareClickableAreas(OneSpanSign.Sdk.ClickableArea sdkClickableArea, OneSpanSign.API.FieldClickableArea apiClickableArea)
        {
            Assert.AreEqual(sdkClickableArea.Width, apiClickableArea.Width);
            Assert.AreEqual(sdkClickableArea.Height, apiClickableArea.Height);
            Assert.AreEqual(sdkClickableArea.Alignment.Value.ToString(), apiClickableArea.Alignment);
        }
    }
}
