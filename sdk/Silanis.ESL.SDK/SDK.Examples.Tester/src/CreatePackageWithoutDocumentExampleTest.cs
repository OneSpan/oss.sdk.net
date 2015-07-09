using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class CreatePackageWithoutDocumentExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            CreatePackageWithoutDocumentExample example = new CreatePackageWithoutDocumentExample(Props.GetInstance());
            example.Run();

            Assert.AreEqual(example.PACKAGE_NAME, example.RetrievedPackage.Name);
        }
    }
}

