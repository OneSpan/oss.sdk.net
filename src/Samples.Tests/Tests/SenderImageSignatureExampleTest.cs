using System;
using NUnit.Framework;
namespace SDK.Examples
{
    [TestFixture()]
    public class SenderImageSignatureExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SenderImageSignatureExample example = new SenderImageSignatureExample();
            example.Run();
            // Verify if the sender image signature was updated correctly.
            Assert.IsNotNull(example.ResultAfterUpdate);
            Assert.AreEqual(example.InputFileContentEncoded, example.ResultAfterUpdate.Content);
            Assert.AreEqual(SenderImageSignatureExample.FILE_NAME, example.ResultAfterUpdate.FileName);
            Assert.AreEqual("image/jpeg", example.ResultAfterUpdate.MediaType);
            // Verify if the sender image signature was deleted correctly.
            Assert.IsNull(example.ResultAfterDelete.Content);
            Assert.IsNull(example.ResultAfterDelete.FileName);
            Assert.IsNull(example.ResultAfterDelete.MediaType);
        }
    }
}
