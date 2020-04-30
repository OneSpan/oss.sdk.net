using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Tests
{
    [TestFixture]
    public class SubAccountBuilderTest
    {
        [Test]
        public void withSpecifiedValues ()
        {
            SubAccountBuilder subAccountBuilder = SubAccountBuilder.NewSubAccount()
                .WithName("newSubAccount")
                .WithLanguage("en")
                .WithTimezoneId("GMT")
                .WithParentAccountId("dummyAccountId");

            SubAccount result = subAccountBuilder.Build();
            

            Assert.AreEqual ( "en", result.Language, "language was not set correctly");
            Assert.AreEqual ( "newSubAccount", result.Name, "Name was not set correctly");
            Assert.AreEqual ( "GMT", result.TimezoneId, "TimezoneId was not set correctly");
            Assert.AreEqual ( "dummyAccountId", result.ParentAccountId, "ParentAccountId was not set correctly");
        }
    }
}