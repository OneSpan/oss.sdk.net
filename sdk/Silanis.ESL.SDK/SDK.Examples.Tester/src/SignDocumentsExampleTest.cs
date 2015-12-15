using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class SignDocumentsExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SignDocumentsExample example = new SignDocumentsExample(Props.GetInstance());
            example.Run();

//            AssertSignatures(example.retrievedPackageBeforeSigning.getDocuments(), example.senderEmail, nullValue(), example.email1, nullValue());
//            Assert.AreEqual(PackageStatus.SENT, example.retrievedPackageBeforeSigning.getStatus());
//
//            AssertSignatures(example.retrievedPackageAfterSigningApproval1.getDocuments(), example.senderEmail, notNullValue(), example.email1, nullValue());
//            Assert.AreEqual(PackageStatus.SENT, example.retrievedPackageAfterSigningApproval1.getStatus());
//
//            AssertSignatures(example.retrievedPackageAfterSigningApproval2.getDocuments(), example.senderEmail, notNullValue(), example.email1, notNullValue());
//            Assert.AreEqual(PackageStatus.COMPLETED, example.retrievedPackageAfterSigningApproval2.getStatus());
        }
    }

//    private void AssertSignatures(IList<Document> documents, string senderEmail, Matcher<Object> senderAccepted, string signerEmail, Matcher<Object> signerAccepted) 
//    {
//        foreach(Document document in documents)
//        {
//            foreach(Signature signature in document.getSignatures()) 
//            {
//                if(senderEmail.equals(signature.getSignerEmail()))
//                    Assert.AreEqual(signature.getAccepted(), senderAccepted);
//                if(signerEmail.equals(signature.getSignerEmail()))
//                    Assert.AreEqual(signature.getAccepted(), signerAccepted);
//            }
//        }
//    }
}

