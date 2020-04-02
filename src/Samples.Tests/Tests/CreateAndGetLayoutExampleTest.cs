using System;
using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture()]
    public class CreateAndGetLayoutExampleTest
    {
        private CreateAndGetLayoutExample example;

        [Test()]
        public void VerifyResult()
        {
            example = new CreateAndGetLayoutExample();
            example.Run();

            Assert.AreEqual(example.LAYOUT_PACKAGE_DESCRIPTION, example.newLayout.Description);
            Assert.AreEqual(1, example.newLayout.Documents.Count);
            Assert.AreEqual(2, example.newLayout.Signers.Count);
            Assert.AreEqual(example.LAYOUT_DOCUMENT_NAME, example.newLayout.Documents[0].Name);
            Assert.AreEqual(1, example.newLayout.Documents[0].Signatures.Count);
        }
    }
}

