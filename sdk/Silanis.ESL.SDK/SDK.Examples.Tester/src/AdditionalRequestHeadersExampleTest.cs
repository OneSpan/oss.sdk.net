using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class AdditionalRequestHeadersExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            AdditionalRequestHeadersExample example = new AdditionalRequestHeadersExample();
            example.Run();

            Assert.AreEqual(DocumentPackageStatus.SENT, example.RetrievedPackage.Status);
        }
    }
}
