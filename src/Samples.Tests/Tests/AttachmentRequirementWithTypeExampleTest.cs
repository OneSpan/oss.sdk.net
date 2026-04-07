using System;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture]
    public class AttachmentRequirementWithTypeExampleTest
    {
        private AttachmentRequirementWithTypeExample example;

        [Test]
        public void VerifyResult()
        {
            example = new AttachmentRequirementWithTypeExample();
            example.Run();

            Assert.IsNotNull(example.retrievedAttachmentRequirement,
                "Retrieved attachment requirement should not be null");
            Assert.AreEqual(example.ATTACHMENT_NAME, example.retrievedAttachmentRequirement.Name,
                "Attachment name was not set correctly");
            Assert.AreEqual(example.ATTACHMENT_DESCRIPTION, example.retrievedAttachmentRequirement.Description,
                "Attachment description was not set correctly");
            Assert.IsTrue(example.retrievedAttachmentRequirement.Required,
                "Attachment should be marked as required");
            Assert.AreEqual(example.ATTACHMENT_TYPE.ToString(), example.retrievedAttachmentRequirement.AttachmentType,
                "Attachment type was not persisted correctly");
            Assert.AreEqual(RequirementStatus.INCOMPLETE.ToString(),
                example.retrievedAttachmentRequirement.Status.ToString(),
                "Attachment status should be INCOMPLETE before signer signs");

            Assert.IsNotNull(example.verificationResults,
                "Verification results list should not be null");

            foreach (AttachmentVerificationResult result in example.verificationResults)
            {
                Assert.IsNotNull(result.AttachmentUuid,
                    "Verification result AttachmentUuid should not be null");
                Assert.IsNotNull(result.FileName,
                    "Verification result FileName should not be null");
            }
        }
    }
}
