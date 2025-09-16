using System.Collections.Generic;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture]
    public class SystemAlertConverterTest
    {
        private SystemAlert sdkSystemAlert1;
        private SystemAlert sdkSystemAlert2;
        private OneSpanSign.API.SystemAlert apiSystemAlert1;
        private OneSpanSign.API.SystemAlert apiSystemAlert2;
        private SystemAlertConverter converter;

        [Test]
        public void ConvertNullSDKToAPI ()
        {
            sdkSystemAlert1 = null;
            converter = new SystemAlertConverter (sdkSystemAlert1);
            Assert.IsNull (converter.ToAPISystemAlert ());
        }

        [Test]
        public void ConvertNullAPIToSDK ()
        {
            apiSystemAlert1 = null;
            converter = new SystemAlertConverter (apiSystemAlert1);
            Assert.IsNull (converter.ToSDKSystemAlert ());
        }

        [Test]
        public void ConvertNullSDKToSDK ()
        {
            sdkSystemAlert1 = null;
            converter = new SystemAlertConverter (sdkSystemAlert1);
            Assert.IsNull (converter.ToSDKSystemAlert ());
        }

        [Test]
        public void ConvertNullAPIToAPI ()
        {
            apiSystemAlert1 = null;
            converter = new SystemAlertConverter (apiSystemAlert1);
            Assert.IsNull (converter.ToAPISystemAlert ());
        }

        [Test]
        public void ConvertSDKToSDK ()
        {
            sdkSystemAlert1 = CreateTypicalSDKSystemAlert ();
            converter = new SystemAlertConverter (sdkSystemAlert1);
            sdkSystemAlert2 = converter.ToSDKSystemAlert ();
            Assert.IsNotNull (sdkSystemAlert2);
            Assert.AreEqual (sdkSystemAlert2, sdkSystemAlert1);
        }

        [Test]
        public void ConvertAPIToAPI ()
        {
            apiSystemAlert1 = CreateTypicalAPISystemAlert ();
            converter = new SystemAlertConverter (apiSystemAlert1);
            apiSystemAlert2 = converter.ToAPISystemAlert ();
            Assert.IsNotNull (apiSystemAlert2);
            Assert.AreEqual (apiSystemAlert2, apiSystemAlert1);
        }

        [Test]
        public void ConvertAPIToSDK ()
        {
            apiSystemAlert1 = CreateTypicalAPISystemAlert ();
            sdkSystemAlert1 = new SystemAlertConverter (apiSystemAlert1).ToSDKSystemAlert ();

            Assert.IsNotNull (sdkSystemAlert1);
            Assert.AreEqual (sdkSystemAlert1.SeverityLevel.getApiValue(), apiSystemAlert1.SeverityLevel.ToString());
            Assert.AreEqual (sdkSystemAlert1.Code, apiSystemAlert1.Code);
            Assert.AreEqual (sdkSystemAlert1.DefaultMessage, apiSystemAlert1.DefaultMessage);
            Assert.AreEqual (sdkSystemAlert1.Parameters.Count, apiSystemAlert1.Parameters.Count);
            Assert.AreEqual (sdkSystemAlert1.Parameters["key1"], apiSystemAlert1.Parameters["key1"]);
            Assert.AreEqual (sdkSystemAlert1.Parameters["key2"], apiSystemAlert1.Parameters["key2"]);
        }

        [Test]
        public void ConvertSDKToAPI ()
        {
            sdkSystemAlert1 = CreateTypicalSDKSystemAlert ();
            apiSystemAlert1 = new SystemAlertConverter (sdkSystemAlert1).ToAPISystemAlert ();

            Assert.IsNotNull (apiSystemAlert1);
            Assert.AreEqual (sdkSystemAlert1.SeverityLevel.getApiValue(), apiSystemAlert1.SeverityLevel.ToString());
            Assert.AreEqual (sdkSystemAlert1.Code, apiSystemAlert1.Code);
            Assert.AreEqual (sdkSystemAlert1.DefaultMessage, apiSystemAlert1.DefaultMessage);
            Assert.AreEqual (sdkSystemAlert1.Parameters.Count, apiSystemAlert1.Parameters.Count);
            Assert.AreEqual (sdkSystemAlert1.Parameters["key1"], apiSystemAlert1.Parameters["key1"]);
            Assert.AreEqual (sdkSystemAlert1.Parameters["key2"], apiSystemAlert1.Parameters["key2"]);
       }

        private SystemAlert CreateTypicalSDKSystemAlert ()
        {
            SystemAlert sdkSystemAlert = new SystemAlert ();
            sdkSystemAlert.SeverityLevel = SeverityLevel.INFO;
            sdkSystemAlert.Code = "Code";
            sdkSystemAlert.DefaultMessage = "default message";
            sdkSystemAlert.Parameters = new Dictionary<string, string>();
            sdkSystemAlert.Parameters.Add ("key1", "value1");
            sdkSystemAlert.Parameters.Add ("key2", "value2");

            return sdkSystemAlert;
        }

        private OneSpanSign.API.SystemAlert CreateTypicalAPISystemAlert ()
        {
            OneSpanSign.API.SystemAlert apiSystemAlert = new OneSpanSign.API.SystemAlert ();

            apiSystemAlert.SeverityLevel = OneSpanSign.API.SeverityLevel.INFO;
            apiSystemAlert.Code = "Code";
            apiSystemAlert.DefaultMessage = "default message";
            apiSystemAlert.Parameters = new Dictionary<string, string>();
            apiSystemAlert.Parameters.Add ("key1", "value1");
            apiSystemAlert.Parameters.Add ("key2", "value2");
            
            return apiSystemAlert;
        }
    }
}
