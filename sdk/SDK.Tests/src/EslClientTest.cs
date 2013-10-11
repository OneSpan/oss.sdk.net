using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

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