using NUnit.Framework;
using System;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class CreateSenderTemplateExampleTest
    {

        [Test()]
        public void VerifyResult()
        {
            CreateSenderTemplateExample example = new CreateSenderTemplateExample();
            example.Run();

            DocumentPackage retrievedTemplate = example.OssClient.GetPackage(example.templateId);

            Assert.AreEqual(retrievedTemplate.Visibility, example.visibility);
        }
    }
}

