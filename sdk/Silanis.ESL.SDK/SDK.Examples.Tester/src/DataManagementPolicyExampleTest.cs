using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
namespace SDK.Examples
{
    [TestFixture ()]
    public class DataManagementPolicyExampleTest
    {
        [Test ()]
        public void VerifyResult ()
        {
            DataManagementPolicyExample example = new DataManagementPolicyExample ();
            example.Run ();

            // Verify if the DataManagementPolicy was updated correctly.
            Assert.IsNotNull (example.DataManagementPolicyAfterUpdate.TransactionRetention);
            Assert.AreEqual (example.DataManagementPolicyAfterUpdate.TransactionRetention.Archived, 60);
            Assert.AreEqual (example.DataManagementPolicyAfterUpdate.TransactionRetention.Completed, 60);
            Assert.AreEqual (example.DataManagementPolicyAfterUpdate.TransactionRetention.Declined, 60);
            Assert.AreEqual (example.DataManagementPolicyAfterUpdate.TransactionRetention.Draft, 60);
            Assert.AreEqual (example.DataManagementPolicyAfterUpdate.TransactionRetention.Expired, 60);
            Assert.AreEqual (example.DataManagementPolicyAfterUpdate.TransactionRetention.OptedOut, 60);
        }
    }
}
