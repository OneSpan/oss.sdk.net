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
            Assert.IsNotNull (example.dataManagementPolicyAfterUpdate.TransactionRetention);
            Assert.AreEqual (example.dataManagementPolicyAfterUpdate.TransactionRetention.Archived, 60);
            Assert.AreEqual (example.dataManagementPolicyAfterUpdate.TransactionRetention.Completed, 60);
            Assert.AreEqual (example.dataManagementPolicyAfterUpdate.TransactionRetention.Declined, 60);
            Assert.AreEqual (example.dataManagementPolicyAfterUpdate.TransactionRetention.Draft, 60);
            Assert.AreEqual (example.dataManagementPolicyAfterUpdate.TransactionRetention.Expired, 60);
            Assert.AreEqual (example.dataManagementPolicyAfterUpdate.TransactionRetention.OptedOut, 60);
        }
    }
}
