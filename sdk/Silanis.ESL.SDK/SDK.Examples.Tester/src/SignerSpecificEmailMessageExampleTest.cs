using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class SignerSpecificEmailMessageExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SignerSpecificEmailMessageExample example = new SignerSpecificEmailMessageExample(Props.GetInstance());
            example.Run();

            DocumentPackage documentPackage = example.EslClient.GetPackage(example.PackageId);

            Assert.AreEqual(documentPackage.Signers[example.Email1].Message, example.EMAIL_MESSAGE);
        }
    }
}

