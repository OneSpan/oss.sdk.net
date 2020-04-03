using NUnit.Framework;
using System;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture()]
    public class DateHelperTest
    {
		private readonly string expectedDateInUTC = "2010-01-01T12:30:00Z";

		[Test()]
        public void TestCase()
        {
			DateTime date = new DateTime(2010, 1, 1, 12, 30, 0, DateTimeKind.Utc);

            string result = DateHelper.dateToIsoUtcFormat(date.ToLocalTime());

			Assert.AreEqual(result, expectedDateInUTC);
        }
    }
}

