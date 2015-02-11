using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class ChangeSignerExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            ChangeSignerExample example = new ChangeSignerExample(Props.GetInstance());
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;
            Assert.IsTrue(documentPackage.Signers[example.email1].CanChangeSigner);
        }
    }
}

