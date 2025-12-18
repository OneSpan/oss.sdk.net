using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class NotificationMethodsForOwnerInAdhocGroupExampleTest
    {
        private string verifyUserId1, verifyUserId2, verifyUserId3;
            
        [Test()]
        public void VerifyResult()
        {
            NotificationMethodsForOwnerInAdhocGroupExample example = new NotificationMethodsForOwnerInAdhocGroupExample();
            example.Run();

            DocumentPackage retrievedPackage = example.RetrievedPackage;

            Signer signer1 = retrievedPackage.GetSigner(example.email1);
            verifyUserId1 = signer1.Id;
            Assert.IsTrue(signer1.NotificationMethods.Primary.Contains(NotificationMethod.EMAIL));
            
            Signer signer2 = retrievedPackage.GetSigner(example.email2);
            verifyUserId2 = signer2.Id;
            Assert.IsTrue(signer2.NotificationMethods.Primary.Contains(NotificationMethod.EMAIL));
            Assert.IsTrue(signer2.NotificationMethods.Primary.Contains(NotificationMethod.SMS));
            Assert.AreEqual(signer2.NotificationMethods.Phone,
                NotificationMethodsForOwnerInAdhocGroupExample.SIGNER_PHONE);

            VerifyAdHocGroupSignerMembers(retrievedPackage, new string[]{ verifyUserId1, verifyUserId2 });

            DocumentPackage updatedPackage = example.updatedPackage;

            Signer ownerSigner = updatedPackage.GetSigner(example.ownerSignerEmail);
            verifyUserId3 = ownerSigner.Id;
            Assert.NotNull(ownerSigner);
            Assert.IsTrue(ownerSigner.NotificationMethods.Primary.Contains(NotificationMethod.EMAIL));
            Assert.IsTrue(ownerSigner.NotificationMethods.Primary.Contains(NotificationMethod.SMS));
            Assert.AreEqual(ownerSigner.NotificationMethods.Phone, NotificationMethodsForOwnerInAdhocGroupExample.OWNER_PHONE);

            VerifyAdHocGroupSignerMembers(updatedPackage, new string[] { verifyUserId1, verifyUserId2, verifyUserId3 });
        }

        private static void VerifyAdHocGroupSignerMembers(DocumentPackage aPackage, string[] expectedOrderedMemberUserIds)
            {
                Signer groupSigner = null;
                foreach (Signer signer in aPackage.Signers)
                {
                    if (signer.IsAdHocGroupSigner())
                    {
                        groupSigner = signer;
                    }
                }
                Assert.NotNull(groupSigner);
                Assert.NotNull(groupSigner.Group);
                Assert.NotNull(groupSigner.Group.Members);
                Assert.AreEqual(groupSigner.Group.Members.Count, expectedOrderedMemberUserIds.Length);
                
                for (int i = 0; i < expectedOrderedMemberUserIds.Length; i++)
                {
                    Assert.AreEqual(groupSigner.Group.Members[i].UserId, expectedOrderedMemberUserIds[i]);  
                }
            }
    }
}
