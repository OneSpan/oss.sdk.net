using NUnit.Framework;
using System;
using OneSpanSign.Sdk;
using OneSpanSign.API;
using OneSpanSign.Sdk.Builder;

namespace SDK.Tests
{
    [TestFixture ()]
    public class SigningLogoConverterTest
    {
        private OneSpanSign.Sdk.SigningLogo sdkSigningLogo1 = null;
        private OneSpanSign.Sdk.SigningLogo sdkSigningLogo2 = null;
        private OneSpanSign.API.SigningLogo apiSigningLogo1 = null;
        private OneSpanSign.API.SigningLogo apiSigningLogo2 = null;
        private SigningLogoConverter converter = null;

        [Test ()]
        public void ConvertNullAPIToSDK ()
        {
            apiSigningLogo1 = null;
            converter = new SigningLogoConverter (apiSigningLogo1);
            Assert.IsNull (converter.ToSDKSigningLogo ());
        }

        [Test ()]
        public void ConvertNullSDKToSDK ()
        {
            sdkSigningLogo1 = null;
            converter = new SigningLogoConverter (sdkSigningLogo1);
            Assert.IsNull (converter.ToSDKSigningLogo ());
        }

        [Test ()]
        public void ConvertSDKToSDK ()
        {
            sdkSigningLogo1 = CreateSDKSigningLogo ();
            converter = new SigningLogoConverter (sdkSigningLogo1);
            sdkSigningLogo2 = converter.ToSDKSigningLogo ();

            Assert.IsNotNull (sdkSigningLogo2);
            Assert.AreEqual (sdkSigningLogo2, sdkSigningLogo1);
        }

        [Test ()]
        public void ConvertAPIToSDK ()
        {
            apiSigningLogo1 = CreateAPISigningLogo ();
            sdkSigningLogo1 = new SigningLogoConverter (apiSigningLogo1).ToSDKSigningLogo ();

            Assert.IsNotNull (sdkSigningLogo1);
            Assert.AreEqual (sdkSigningLogo1.Language, apiSigningLogo1.Language);
            Assert.AreEqual (sdkSigningLogo1.Image, apiSigningLogo1.Image);          
        }

        [Test ()]
        public void ConvertNullSDKToAPI ()
        {
            sdkSigningLogo1 = null;
            converter = new SigningLogoConverter (sdkSigningLogo1);
            Assert.IsNull (converter.ToAPISigningLogo ());
        }

        [Test ()]
        public void ConvertNullAPIToAPI ()
        {
            apiSigningLogo1 = null;
            converter = new SigningLogoConverter (apiSigningLogo1);

            Assert.IsNull (converter.ToAPISigningLogo ());
        }

        [Test ()]
        public void ConvertAPIToAPI ()
        {
            apiSigningLogo1 = CreateAPISigningLogo ();
            converter = new SigningLogoConverter (apiSigningLogo1);
            apiSigningLogo2 = converter.ToAPISigningLogo ();

            Assert.IsNotNull (apiSigningLogo2);
            Assert.AreEqual (apiSigningLogo2, apiSigningLogo1);
        }

        [Test ()]
        public void ConvertSDKToAPI ()
        {
            sdkSigningLogo1 = CreateSDKSigningLogo ();
            apiSigningLogo1 = new SigningLogoConverter (sdkSigningLogo1).ToAPISigningLogo ();

            Assert.IsNotNull (apiSigningLogo1);
            Assert.AreEqual (apiSigningLogo1.Language, sdkSigningLogo1.Language);
            Assert.AreEqual (apiSigningLogo1.Image, sdkSigningLogo1.Image);          
        }

        private OneSpanSign.Sdk.SigningLogo CreateSDKSigningLogo ()
        {
            return SigningLogoBuilder.NewSigningLogo ()
                 .WithLanguage ("en")
                 .WithImage ("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEUAAABnCAYAAABW6Y8UAAAABGdBTUEAALGPC")
                 .Build ();           
        }

        private OneSpanSign.API.SigningLogo CreateAPISigningLogo ()
        {
            OneSpanSign.API.SigningLogo result = new OneSpanSign.API.SigningLogo ();
            result.Language = "en";
            result.Image = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEUAAABnCAYAAABW6Y8UAAAABGdBTUEAALGPC\"";
            return result;
        }
    }
}

