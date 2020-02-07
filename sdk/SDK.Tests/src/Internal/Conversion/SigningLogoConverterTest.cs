using NUnit.Framework;
using System;
using Silanis.ESL.SDK;
using Silanis.ESL.API;
using Silanis.ESL.SDK.Builder;

namespace SDK.Tests
{
    [TestFixture ()]
    public class SigningLogoConverterTest
    {
        private Silanis.ESL.SDK.SigningLogo sdkSigningLogo1 = null;
        private Silanis.ESL.SDK.SigningLogo sdkSigningLogo2 = null;
        private Silanis.ESL.API.SigningLogo apiSigningLogo1 = null;
        private Silanis.ESL.API.SigningLogo apiSigningLogo2 = null;
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

        private Silanis.ESL.SDK.SigningLogo CreateSDKSigningLogo ()
        {
            return SigningLogoBuilder.NewSigningLogo ()
                 .WithLanguage ("en")
                 .WithImage ("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEUAAABnCAYAAABW6Y8UAAAABGdBTUEAALGPC")
                 .Build ();           
        }

        private Silanis.ESL.API.SigningLogo CreateAPISigningLogo ()
        {
            Silanis.ESL.API.SigningLogo result = new Silanis.ESL.API.SigningLogo ();
            result.Language = "en";
            result.Image = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEUAAABnCAYAAABW6Y8UAAAABGdBTUEAALGPC\"";
            return result;
        }
    }
}

