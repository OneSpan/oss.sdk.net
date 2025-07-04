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

            Assert.AreEqual(example.CreatedPackage.GetSigner(example.email1).NotificationMethods.Primary,
                example.UpdatedPackage.GetSigner(example.email1).NotificationMethods.Primary,
                "Signer's primary notification methods shouldn't have been updated.");
        }
    }
}