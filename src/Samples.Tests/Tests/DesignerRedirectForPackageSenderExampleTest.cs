using NUnit.Framework;
using System;
using OneSpanSign.Sdk.Internal;

namespace SDK.Examples
{
    [TestFixture()]
    public class DesignerRedirectForPackageSenderExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            DesignerRedirectForPackageSenderExample example = new DesignerRedirectForPackageSenderExample();
            example.Run();

            Assert.IsNotEmpty(example.GeneratedLinkToDesignerForSender);
        }
    }
}

