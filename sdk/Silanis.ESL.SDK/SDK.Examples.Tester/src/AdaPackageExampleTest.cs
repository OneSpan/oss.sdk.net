using System;
using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture()]
    public class AdaPackageExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            AdaPackageExample example = new AdaPackageExample();
            example.Run();

            Assert.IsTrue(example.RetrievedPackage.GetDocument(example.DOCUMENT_NAME).Tagged.Value);
        }
    }
}

