using NUnit.Framework;
using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    [TestFixture()]
    public class GetSigningStatusExampleTest
    {

        private const String CURRENT_SIGNER_STATUS = "CURRENT_SIGNER";

        [Test()]
        public void VerifyResult()
        {
            GetSigningStatusExample example = new GetSigningStatusExample(Props.GetInstance());
            example.Run();

            String signerEmail = Props.GetInstance().Get( "1.email" );

            DocumentPackage documentPackage = example.EslClient.GetPackage(example.PackageId);
            Signer signer = documentPackage.Signers[signerEmail];


            Assert.AreEqual(signer.Status, CURRENT_SIGNER_STATUS);

        }
    }
}

