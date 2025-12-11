using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class NotificationMethodsForAdhocGroupExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            NotificationMethodsForAdhocGroupExample example = new NotificationMethodsForAdhocGroupExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            Assert.IsTrue(documentPackage.GetSigner(example.email1).NotificationMethods.Primary.Contains(NotificationMethod.EMAIL));
            
            Assert.IsTrue(documentPackage.GetSigner(example.email2).NotificationMethods.Primary.Contains(NotificationMethod.EMAIL));
            Assert.IsTrue(documentPackage.GetSigner(example.email2).NotificationMethods.Primary.Contains(NotificationMethod.SMS));
          
            Assert.AreEqual(documentPackage.GetSigner(example.email2).NotificationMethods.Phone, "+12042345678");
        }
    }
}