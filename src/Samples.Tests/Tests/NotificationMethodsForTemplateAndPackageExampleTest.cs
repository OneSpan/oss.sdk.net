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
            DocumentPackage createdPackage = example.CreatedPackage;
            DocumentPackage signerUpdatedPackage = example.SignerUpdatedPackage;


            Assert.AreEqual(templatePackage.GetSigner(example.email1).NotificationMethods.Phone, PACKAGE_SIGNER1_PHONE);
            Assert.AreEqual(createdPackage.GetSigner(example.email1).NotificationMethods.Phone, PACKAGE_SIGNER1_PHONE);
            Assert.AreEqual(signerUpdatedPackage.GetSigner(example.email1).NotificationMethods.Phone, "+15147623743");

            CollectionAssert.Contains(templatePackage.GetSigner(example.email1).NotificationMethods.Primary, NotificationMethod.EMAIL);
            CollectionAssert.Contains(templatePackage.GetSigner(example.email1).NotificationMethods.Primary, NotificationMethod.SMS);

            CollectionAssert.Contains(createdPackage.GetSigner(example.email1).NotificationMethods.Primary, NotificationMethod.EMAIL);
            CollectionAssert.Contains(createdPackage.GetSigner(example.email1).NotificationMethods.Primary, NotificationMethod.SMS);

            CollectionAssert.Contains(signerUpdatedPackage.GetSigner(example.email1).NotificationMethods.Primary, NotificationMethod.EMAIL);
            CollectionAssert.DoesNotContain(signerUpdatedPackage.GetSigner(example.email1).NotificationMethods.Primary, NotificationMethod.SMS);
        }
    }
}