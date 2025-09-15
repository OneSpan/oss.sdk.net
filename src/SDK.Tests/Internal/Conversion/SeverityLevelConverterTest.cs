using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    public class SeverityLevelConverterTest
    {
        private SeverityLevel sdkSeverityLevel;
        private OneSpanSign.API.SeverityLevel apiSeverityLevel;

		[Test]
		public void ConvertAPIToSDK()
		{
            apiSeverityLevel = OneSpanSign.API.SeverityLevel.INFO;
            sdkSeverityLevel = new SeverityLevelConverter(apiSeverityLevel).ToSDKSeverityLevel();

			Assert.AreEqual(apiSeverityLevel.ToString(), sdkSeverityLevel.ToString());
            
            apiSeverityLevel = OneSpanSign.API.SeverityLevel.WARNING;
            sdkSeverityLevel = new SeverityLevelConverter(apiSeverityLevel).ToSDKSeverityLevel();
            
            Assert.AreEqual(apiSeverityLevel.ToString(), sdkSeverityLevel.ToString());
            
            apiSeverityLevel = OneSpanSign.API.SeverityLevel.CRITICAL;
            sdkSeverityLevel = new SeverityLevelConverter(apiSeverityLevel).ToSDKSeverityLevel();
            
            Assert.AreEqual(apiSeverityLevel.ToString(), sdkSeverityLevel.ToString());
		}
        
        [Test]
        public void ConvertSDKToAPI()
        {
            sdkSeverityLevel = SeverityLevel.INFO;
            apiSeverityLevel = new SeverityLevelConverter(sdkSeverityLevel).ToAPISeverityLevel();

            Assert.AreEqual(apiSeverityLevel.ToString(), sdkSeverityLevel.ToString());
            
            sdkSeverityLevel = SeverityLevel.WARNING;
            apiSeverityLevel = new SeverityLevelConverter(sdkSeverityLevel).ToAPISeverityLevel();
            
            Assert.AreEqual(apiSeverityLevel.ToString(), sdkSeverityLevel.ToString());
            
            sdkSeverityLevel = SeverityLevel.CRITICAL;
            apiSeverityLevel = new SeverityLevelConverter(sdkSeverityLevel).ToAPISeverityLevel();
            
            Assert.AreEqual(apiSeverityLevel.ToString(), sdkSeverityLevel.ToString());
        }
    }
}
