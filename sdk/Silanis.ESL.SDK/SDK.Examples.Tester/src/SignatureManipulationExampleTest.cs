using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class SignatureManipulationExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SignatureManipulationExample example = new SignatureManipulationExample(Props.GetInstance());
            example.Run();

            PackageId packageId = example.PackageId;
            DocumentPackage createdPackage = example.EslClient.GetPackage(packageId);
            Document sdkDocument = null;
            Signature signature1 = null;
            Signature signature2 = null;

            createdPackage.Documents.TryGetValue("First Document", out sdkDocument);

            if (sdkDocument != null)
            {
                foreach(Signature signature in sdkDocument.Signatures)
                {
                    if (signature.Id.Id.Equals("signatureId1"))
                    {
                        signature1 = signature;
                    }
                    if (signature.Id.Id.Equals("signatureId2"))
                    {
                        signature2 = signature;
                    }
                }
            }

            Assert.IsNotNull(sdkDocument);
            Assert.IsNull(signature1);
            Assert.IsNotNull(signature2);

        }
    }
}

