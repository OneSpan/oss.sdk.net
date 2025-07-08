using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class UpdatePackagePreservesNotificationMethodsExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            UpdatePackagePreservesNotificationMethodsExample example = new UpdatePackagePreservesNotificationMethodsExample();
            example.Run();

            Assert.AreEqual("+12042345678", example.CreatedPackage.GetSigner(example.email1).NotificationMethods.Phone, "Signer's primary notification methods is not set correctly");
            CollectionAssert.Contains(example.CreatedPackage.GetSigner(example.email1).NotificationMethods.Primary, NotificationMethod.EMAIL);
            CollectionAssert.Contains(example.CreatedPackage.GetSigner(example.email1).NotificationMethods.Primary, NotificationMethod.SMS);
            

            CollectionAssert.AreEqual(example.CreatedPackage.GetSigner(example.email1).NotificationMethods.Primary,
                example.UpdatedPackage.GetSigner(example.email1).NotificationMethods.Primary,
                "Signer's primary notification methods shouldn't have been updated.");

            Assert.AreEqual(example.CreatedPackage.GetSigner(example.email1).NotificationMethods.Phone,
                example.UpdatedPackage.GetSigner(example.email1).NotificationMethods.Phone,
                "Signer's notification phone number shouldn't have been updated.");
        }
    }
}