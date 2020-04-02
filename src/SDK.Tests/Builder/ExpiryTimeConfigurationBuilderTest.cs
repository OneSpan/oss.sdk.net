using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
namespace SDK.Tests
{
    [TestFixture]
    public class ExpiryTimeConfigurationBuilderTest
    {
        [Test]
        public void BuildTest ()
        {
            ExpiryTimeConfigurationBuilder builder = ExpiryTimeConfigurationBuilder.NewExpiryTimeConfiguration ()
                    .WithMaximumRemainingDays (90)
                    .WithRemainingDays (80);

            ExpiryTimeConfiguration result = builder.Build ();

            Assert.IsNotNull (result);
            Assert.AreEqual (result.MaximumRemainingDays, 90);
            Assert.AreEqual (result.RemainingDays, 80);
        }
    }
}
