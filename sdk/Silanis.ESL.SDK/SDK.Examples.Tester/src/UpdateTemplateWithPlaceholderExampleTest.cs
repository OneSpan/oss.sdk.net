using System;
using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture()]
    public class UpdateTemplateWithPlaceholderExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            UpdateTemplateWithPlaceholderExample example = new UpdateTemplateWithPlaceholderExample(Props.GetInstance());
            example.Run();

            Assert.AreEqual(example.TEMPLATE_NAME, example.retrievedTemplate.Name);
            Assert.AreEqual(2, example.retrievedTemplate.Signers.Count);
            Assert.AreEqual(1, example.retrievedTemplate.Placeholders.Count);
            Assert.IsNotNull(example.retrievedTemplate.Placeholders[0]);
            Assert.AreEqual(2, example.retrievedTemplate.Documents[example.DOCUMENT_NAME].Signatures.Count);

            Assert.AreEqual(2, example.updatedTemplate.Signers.Count);
            Assert.AreEqual(2, example.updatedTemplate.Placeholders.Count);
            Assert.IsNotNull(example.updatedTemplate.Placeholders[0]);
            Assert.IsNotNull(example.updatedTemplate.Placeholders[1]);
            Assert.AreEqual(3, example.updatedTemplate.Documents[example.DOCUMENT_NAME].Signatures.Count);
        }
    }
}

