using System;
using Silanis.ESL.SDK;
using NUnit.Framework;

namespace SDK.Tests
{
	[TestFixture]
	public class EslClientTest
	{
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