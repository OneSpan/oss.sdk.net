using NUnit.Framework;
using System;

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
        }
    }
}

