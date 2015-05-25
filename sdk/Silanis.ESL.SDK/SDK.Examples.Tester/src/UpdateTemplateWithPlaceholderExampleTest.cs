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

            Assert.AreEqual(example.TEMPLATE1_NAME, example.retrievedTemplate.Name);
            Assert.AreEqual(2, example.retrievedTemplate.Signers.Count);
            Assert.AreEqual(1, example.retrievedTemplate.Placeholders.Count);
            Assert.IsNotNull(example.retrievedTemplate.Placeholders[example.PLACEHOLDER_ID]);

            Assert.AreEqual(example.TEMPLATE2_NAME, example.updatedTemplate.Name);
            Assert.AreEqual(2, example.updatedTemplate.Signers.Count);
            Assert.AreEqual(2, example.updatedTemplate.Placeholders.Count);
            Assert.IsNotNull(example.updatedTemplate.Placeholders[example.PLACEHOLDER_ID]);
            Assert.IsNotNull(example.updatedTemplate.Placeholders[example.PLACEHOLDER2_ID]);
        }
    }
}

