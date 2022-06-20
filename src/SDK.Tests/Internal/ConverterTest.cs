using NUnit.Framework;
using System;
using OneSpanSign.Sdk.Internal;

namespace SDK.Tests
{
    [TestFixture()]
    public class ConverterTest
    {
		public static readonly string jenkinsApiKey = "amVua2luc1VzZXJJZDpCc2JwMnlzSUFEZ0g=";   //A fake Apikey only for test purpose
		public static readonly string expectedJenkinsUID = "jenkinsUserId";

		[Test()]
		public void apiKeyToUIDTest()
        {
			string result = Converter.apiKeyToUID(jenkinsApiKey);

			Assert.AreEqual(expectedJenkinsUID, result);
        }
    }
}

