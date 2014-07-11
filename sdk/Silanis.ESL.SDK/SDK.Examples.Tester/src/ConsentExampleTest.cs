using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class ConsentExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            ConsentExample example = new ConsentExample(Props.GetInstance());
            example.Run();

            DocumentPackage documentPackage = example.EslClient.GetPackage(example.PackageId);

            // Verify if the required information is correctly extracted.
            Document document = documentPackage.Documents["Custom Consent Document"];

            Assert.AreEqual(document.Signatures[0].Style, SignatureStyle.ACCEPTANCE);
        }
    }
}

