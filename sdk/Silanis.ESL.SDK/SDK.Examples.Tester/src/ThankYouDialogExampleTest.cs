using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class ThankYouDialogExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            ThankYouDialogExample example = new ThankYouDialogExample(Props.GetInstance());
            example.Run();

            Assert.IsNotNull(example.thankYouDialogContent);
            Assert.IsNotEmpty(example.thankYouDialogContent);
        }
    }
}

