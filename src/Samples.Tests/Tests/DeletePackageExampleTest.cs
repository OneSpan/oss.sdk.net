using NUnit.Framework;
using System;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class DeletePackageExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            Assert.Throws<OssServerException>(() =>
            {
                DeletePackageExample example = new DeletePackageExample();
                example.Run();

                Assert.IsNull(example.RetrievedPackage);
            });
        }
    }
}

