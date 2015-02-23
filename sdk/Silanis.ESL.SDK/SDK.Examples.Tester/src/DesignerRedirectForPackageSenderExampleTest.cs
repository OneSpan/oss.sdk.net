using NUnit.Framework;
using System;
using Silanis.ESL.SDK.Internal;

namespace SDK.Examples
{
    [TestFixture()]
    public class DesignerRedirectForPackageSenderExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            DesignerRedirectForPackageSenderExample example = new DesignerRedirectForPackageSenderExample(Props.GetInstance());
            example.Run();

            Assert.IsNotNull(example.GeneratedLinkToDesignerForSender);

            string stringResponse = HttpRequestUtil.GetUrlContent(example.GeneratedLinkToDesignerForSender);
            StringAssert.Contains("Electronic Disclosures and Signatures Consent", stringResponse);
        }
    }
}

