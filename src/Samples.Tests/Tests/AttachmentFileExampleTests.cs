using System;
using NUnit.Framework;
namespace SDK.Examples
{
    [TestFixture]
    public class AttachmentFileExampleTests
    {
        private AttachmentFileExample example = new AttachmentFileExample ();

        [Test]
        public void VerifyResult ()
        {
            example.Run ();
            Assert.AreEqual (example.filesAfterUpload.Count, 1);
            Assert.AreEqual (example.filesAfterDelete.Count, 0);

            Assert.AreEqual(example.ATTACHMENT_FILE_NAME, example.downloadedAttachmentFile.Name);
            Assert.AreEqual(example.signerAttachmentFileSize, example.downloadedAttachmentFile.Length);
        }
    }
}
