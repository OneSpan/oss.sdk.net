using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class CustomSenderInfoInCreateNewTemplateExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            CustomSenderInfoInCreateNewTemplateExample example = new CustomSenderInfoInCreateNewTemplateExample(Props.GetInstance());
            example.Run();
            
            DocumentPackage template = example.EslClient.GetPackage( example.TemplateId );
            
            Assert.IsNotNull(template.SenderInfo);
        }
    }
}

