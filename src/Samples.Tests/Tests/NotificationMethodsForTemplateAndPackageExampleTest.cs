using NUnit.Framework;
using OneSpanSign.Sdk;
using static SDK.Examples.NotificationMethodsForTemplateAndPackageExample;

namespace SDK.Examples
{
    [TestFixture()]
    public class NotificationMethodsForTemplateAndPackageExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            NotificationMethodsForTemplateAndPackageExample example = new NotificationMethodsForTemplateAndPackageExample();
            example.Run();

            DocumentPackage templatePackage = example.TemplatePackage;
            DocumentPackage updatedPackage = example.UpdatedPackage;
            DocumentPackage signerUpdatedPackage = example.SignerUpdatedPackage;


            Assert.AreEqual(PACKAGE_SIGNER1_PHONE, templatePackage.GetSigner(example.email1).NotificationMethods.Phone);
            Assert.AreEqual(PACKAGE_SIGNER1_PHONE, updatedPackage.GetSigner(example.email1).NotificationMethods.Phone);
            Assert.AreEqual(PACKAGE_SIGNER1_PHONE, signerUpdatedPackage.GetSigner(example.email1).NotificationMethods.Phone);

            CollectionAssert.Contains(templatePackage.GetSigner(example.email1).NotificationMethods.Primary, NotificationMethod.EMAIL);
            CollectionAssert.Contains(templatePackage.GetSigner(example.email1).NotificationMethods.Primary, NotificationMethod.SMS);

            CollectionAssert.Contains(updatedPackage.GetSigner(example.email1).NotificationMethods.Primary, NotificationMethod.EMAIL);
            CollectionAssert.Contains(updatedPackage.GetSigner(example.email1).NotificationMethods.Primary, NotificationMethod.SMS);

            CollectionAssert.Contains(signerUpdatedPackage.GetSigner(example.email1).NotificationMethods.Primary, NotificationMethod.EMAIL);        }
    }
}