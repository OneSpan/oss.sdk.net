using System;
using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture()]
    public class PackageViewRedirectForPackageSenderExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            PackageViewRedirectForPackageSenderExample example = new PackageViewRedirectForPackageSenderExample(Props.GetInstance());
            example.Run();

            Assert.IsNotNull(example.generatedLinkToPackageViewForSender);
        }
    }
}

