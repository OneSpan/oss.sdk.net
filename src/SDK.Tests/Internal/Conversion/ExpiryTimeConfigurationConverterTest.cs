using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
namespace SDK.Tests
{
    [TestFixture ()]
    public class ExpiryTimeConfigurationConverterTest
    {

        private OneSpanSign.Sdk.ExpiryTimeConfiguration sdkExpiryTimeConfiguration1 = null;
        private OneSpanSign.Sdk.ExpiryTimeConfiguration sdkExpiryTimeConfiguration2 = null;
        private OneSpanSign.API.ExpiryTimeConfiguration apiExpiryTimeConfiguration1 = null;
        private OneSpanSign.API.ExpiryTimeConfiguration apiExpiryTimeConfiguration2 = null;
        private ExpiryTimeConfigurationConverter converter = null;

        [Test ()]
        public void convertNullSDKToAPI ()
        {
            sdkExpiryTimeConfiguration1 = null;
            converter = new ExpiryTimeConfigurationConverter (sdkExpiryTimeConfiguration1);
            Assert.IsNull (converter.ToAPIExpiryTimeConfiguration());

        }

        [Test ()]
        public void convertNullAPIToSDK ()
        {
            apiExpiryTimeConfiguration1 = null;
            converter = new ExpiryTimeConfigurationConverter (apiExpiryTimeConfiguration1);
            Assert.IsNull (converter.ToSDKExpiryTimeConfiguration());

        }

        [Test ()]
        public void convertNullSDKToSDK ()
        {
            sdkExpiryTimeConfiguration1 = null;
            converter = new ExpiryTimeConfigurationConverter (sdkExpiryTimeConfiguration1);
            Assert.IsNull (converter.ToSDKExpiryTimeConfiguration ());

        }

        [Test ()]
        public void convertNullAPIToAPI ()
        {
            apiExpiryTimeConfiguration1 = null;
            converter = new ExpiryTimeConfigurationConverter (apiExpiryTimeConfiguration1);
            Assert.IsNull (converter.ToAPIExpiryTimeConfiguration ());

        }

        [Test ()]
        public void convertSDKToSDK ()
        {
            sdkExpiryTimeConfiguration1 = new ExpiryTimeConfiguration ();
            sdkExpiryTimeConfiguration2 = new ExpiryTimeConfigurationConverter (sdkExpiryTimeConfiguration1).ToSDKExpiryTimeConfiguration ();
            Assert.IsNotNull (sdkExpiryTimeConfiguration2);
            Assert.AreEqual (sdkExpiryTimeConfiguration2, sdkExpiryTimeConfiguration1);
        }

        [Test ()]
        public void convertAPIToAPI ()
        {
            apiExpiryTimeConfiguration1 = new OneSpanSign.API.ExpiryTimeConfiguration ();
            apiExpiryTimeConfiguration2 = new ExpiryTimeConfigurationConverter (apiExpiryTimeConfiguration1).ToAPIExpiryTimeConfiguration ();

            Assert.IsNotNull (apiExpiryTimeConfiguration2);
            Assert.AreEqual (apiExpiryTimeConfiguration2, apiExpiryTimeConfiguration1);

        }

        [Test ()]
        public void convertAPIToSDK ()
        {
            apiExpiryTimeConfiguration1 = new OneSpanSign.API.ExpiryTimeConfiguration ();
            apiExpiryTimeConfiguration1.RemainingDays = 80;
            apiExpiryTimeConfiguration1.MaximumRemainingDays = 90;
            sdkExpiryTimeConfiguration1 = new ExpiryTimeConfigurationConverter (apiExpiryTimeConfiguration1).ToSDKExpiryTimeConfiguration ();

            Assert.IsNotNull (sdkExpiryTimeConfiguration1);
            Assert.AreEqual (apiExpiryTimeConfiguration1.MaximumRemainingDays, sdkExpiryTimeConfiguration1.MaximumRemainingDays);
            Assert.AreEqual (apiExpiryTimeConfiguration1.RemainingDays, sdkExpiryTimeConfiguration1.RemainingDays);
        }

        [Test ()]
        public void convertSDKToAPI ()
        {
            sdkExpiryTimeConfiguration1 = new ExpiryTimeConfiguration ();
            sdkExpiryTimeConfiguration1.RemainingDays = 80;
            sdkExpiryTimeConfiguration1.MaximumRemainingDays = 90;
            apiExpiryTimeConfiguration1 = new ExpiryTimeConfigurationConverter (sdkExpiryTimeConfiguration1).ToAPIExpiryTimeConfiguration ();

            Assert.IsNotNull (apiExpiryTimeConfiguration1);
            Assert.AreEqual (sdkExpiryTimeConfiguration1.MaximumRemainingDays, apiExpiryTimeConfiguration1.MaximumRemainingDays);
            Assert.AreEqual (sdkExpiryTimeConfiguration1.RemainingDays, apiExpiryTimeConfiguration1.RemainingDays);
        }
    }
}
