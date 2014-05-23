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
//            Assert.AreEqual(CustomSenderInfoExample.SENDER_FIRST_NAME, package.SenderInfo.FirstName);
//            Assert.AreEqual(CustomSenderInfoExample.SENDER_SECOND_NAME, package.SenderInfo.LastName);
//            Assert.AreEqual(CustomSenderInfoExample.SENDER_COMPANY, package.SenderInfo.Company);
//            Assert.AreEqual(CustomSenderInfoExample.SENDER_TITLE, package.SenderInfo.Title);
        }
    }
}

