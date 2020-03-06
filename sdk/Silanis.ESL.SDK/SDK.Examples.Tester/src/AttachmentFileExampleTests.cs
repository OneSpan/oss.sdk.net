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

        }
    }
}
