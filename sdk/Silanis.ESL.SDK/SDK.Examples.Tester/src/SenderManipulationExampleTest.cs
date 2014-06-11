using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class SenderManipulationExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SenderManipulationExample example = new SenderManipulationExample(Props.GetInstance());
            example.Run();

            // Invite Account Members
            Assert.IsTrue(example.accountMembers.ContainsKey(example.email1));
            Assert.IsTrue(example.accountMembers.ContainsKey(example.email2));
            Assert.IsTrue(example.accountMembers.ContainsKey(example.email3));

            // Delete Sender
            Assert.IsFalse(example.accountMembersWithDeletedSender.ContainsKey(example.email2));

            // Update Sender
            Sender sender = example.accountMembersWithUpdatedSender[example.email3];
            SenderInfo updatedInfo = example.updatedSenderInfo;

            Assert.AreEqual(updatedInfo.FirstName, sender.FirstName);
            Assert.AreEqual(updatedInfo.LastName, sender.LastName);
            Assert.AreEqual(updatedInfo.Company, sender.Company);
            Assert.AreEqual(updatedInfo.Title, sender.Title);
        }
    }
}
