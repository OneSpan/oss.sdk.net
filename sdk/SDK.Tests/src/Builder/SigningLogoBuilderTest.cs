using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Tests
{
    [TestFixture]
    public class SigningLogoBuilderTest
    {
        [Test]
        public void withSpecifiedValues ()
        {
            SigningLogoBuilder signingLogoBuilder = SigningLogoBuilder.NewSigningLogo ()
                 .WithLanguage ("en")
                 .WithImage ("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEUAAABnCA");

            SigningLogo result = signingLogoBuilder.Build ();

            Assert.AreEqual ( "en", result.Language, "language was not set correctly");
            Assert.AreEqual ( "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEUAAABnCA", result.Image, "image was not set correctly");
        }
    }
}