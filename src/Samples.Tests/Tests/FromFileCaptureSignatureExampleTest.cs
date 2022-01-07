using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture()]
    public class FromFileCaptureSignatureExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            FromFileCaptureSignatureExample example = new FromFileCaptureSignatureExample();
            example.Run();

            Assert.AreEqual(example.RetrievedPackage.Documents[1].Signatures[0].FromFile, true);
        }
    }
}
