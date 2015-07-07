using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class SignerOrderingExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SignerOrderingExample example = new SignerOrderingExample( Props.GetInstance() );
            example.Run();

            // Initial signing order
            DocumentPackage beforeReorder = example.savedPackage;
            Assert.AreEqual(beforeReorder.Signers[1].Email, example.email1);
            Assert.AreEqual(beforeReorder.Signers[2].Email, example.email2);

            // After reordering signers
            DocumentPackage afterReorder = example.afterReorder;
            Assert.AreEqual(afterReorder.Signers[1].Email, example.email2);
            Assert.AreEqual(afterReorder.Signers[2].Email, example.email1);
        }
    }
}

