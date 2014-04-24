using NUnit.Framework;
using System;

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
        }
    }
}

