using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
namespace SDK.Examples
{

    [TestFixture ()]
    public class ExpiryTimeConfigurationExampleTest
    {
        [Test ()]
        public void VerifyResult ()
        {
            ExpiryTimeConfigurationExample example = new ExpiryTimeConfigurationExample ();
            example.Run ();

            // Verify if the ExpiryTimeConfiguration was updated correctly.
            Assert.AreEqual (example.ExpiryTimeConfigurationAfterUpdate.MaximumRemainingDays, 60);
            Assert.AreEqual (example.ExpiryTimeConfigurationAfterUpdate.RemainingDays, 60);
        }
    }
}
