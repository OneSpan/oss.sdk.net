using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture ()]
    public class SignatureLayoutExampleTest
    {
        [Test ()]
        public void VerifyResult ()
        {
            SignatureLayoutExample example = new SignatureLayoutExample();
            example.Run ();
            
            Assert.NotNull(example.PatchedSignatureLayout.Logo);
            Assert.AreEqual(example.SIGNATURE_LOGO_IMAGE_BASE64,example.PatchedSignatureLayout.Logo.Image);
            
        }
    }
}