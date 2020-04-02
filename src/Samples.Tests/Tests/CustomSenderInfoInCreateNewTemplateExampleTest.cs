using NUnit.Framework;
using System;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class CustomSenderInfoInCreateNewTemplateExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            CustomSenderInfoInCreateNewTemplateExample example = new CustomSenderInfoInCreateNewTemplateExample();
            example.Run();
            
            DocumentPackage template = example.OssClient.GetPackage( example.TemplateId );
            
            Assert.IsNotNull(template.SenderInfo);
        }
    }
}

