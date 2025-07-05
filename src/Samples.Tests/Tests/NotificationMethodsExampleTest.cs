using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class NotificationMethodsExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            NotificationMethodsExample example = new NotificationMethodsExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            Assert.IsNull(documentPackage.GetSigner(example.email1).NotificationMethods);

            Assert.IsTrue(documentPackage.GetSigner(example.email2).NotificationMethods.Primary.Contains(NotificationMethod.EMAIL));
            
            Assert.IsTrue(documentPackage.GetSigner(example.email3).NotificationMethods.Primary.Contains(NotificationMethod.EMAIL));
            Assert.IsTrue(documentPackage.GetSigner(example.email3).NotificationMethods.Primary.Contains(NotificationMethod.SMS));
          
            Assert.AreEqual(documentPackage.GetSigner(example.email3).NotificationMethods.Phone, "+12042345678");
        }
    }
}