using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
	[TestFixture]
	public class EslClientTest
	{
		[Test]
		public void AppendsServicePathToURL()
		{
			Assert.AreEqual ("http://localhost/aws/rest/services", new EslClient ("API KEY", "http://localhost").BaseUrl);
			Assert.AreEqual ("http://localhost/aws/rest/services", new EslClient ("API KEY", "http://localhost/").BaseUrl);
		}

		[Test]
		[ExpectedException(typeof(EslException))]
		public void CannotCreateClientWithNullAPIKey()
		{
			new EslClient (null, "http://localhost");
		}

		[Test]
		[ExpectedException(typeof(EslException))]
		public void CannotCreateClientWithNullURL() 
		{
			new EslClient ("key", null);
		}
	}
}