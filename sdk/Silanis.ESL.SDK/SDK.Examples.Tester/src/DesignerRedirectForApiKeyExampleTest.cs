using NUnit.Framework;
using System;
using Silanis.ESL.SDK.Internal;

namespace SDK.Examples
{
    [TestFixture()]
    public class DesignerRedirectForApiKeyExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            DesignerRedirectForApiKeyExample example = new DesignerRedirectForApiKeyExample(Props.GetInstance());
            example.Run();

            Assert.IsNotNull(example.GeneratedLinkToDesignerForApiKey);

            string stringResponse = HttpRequestUtil.GetUrlContent(example.GeneratedLinkToDesignerForApiKey);
            StringAssert.Contains("Electronic Disclosures and Signatures Consent", stringResponse);
        }
    }
}

